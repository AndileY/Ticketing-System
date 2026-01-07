using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Security.Claims;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.Ticket;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;
        private readonly UserManager<User> _userManager;

        public TicketController(IMapper mapper, TicketSystemDbContext context, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.context = context;
            _userManager = userManager;
        }
        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketReadOnlyDto>>> GetAllTicket()
        {
            var ticket = await context.Tickets.ToListAsync();
            var tickets = mapper.Map<IEnumerable<TicketReadOnlyDto>>(ticket);
            return Ok(tickets);

          
        }

     

        [HttpGet("{id}")]
        [Authorize] // anyone logged in can hit, but we check below
        public async Task<ActionResult<TicketDetailsDto>> GetTicket(int id)
        {
            var ticket = await context.Tickets
                .Include(t => t.AssignedTo)
                .Include(t => t.ClientAccount)
                
                .FirstOrDefaultAsync(t => t.TicketId == id);

            if (ticket == null)
                return NotFound("Ticket not found.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If the logged-in user is a Client
            if (User.IsInRole("Client"))
            {
                if (ticket.UserId != userId)
                {
                    return Forbid("You are not allowed to view this ticket.");
                }
            }

            var ticketsDto = mapper.Map<TicketDetailsDto>(ticket);
            return Ok(ticketsDto);
        }





        [HttpPost("create")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> CreateTicket([FromBody] TicketCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not found.");

            // Get the client account
            var clientAccount = await  context.ClientAccounts
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (clientAccount == null)
                return BadRequest("Client account not linked to your user.");

            // 1️⃣ Create the ticket
            var ticket = new Ticket
            {
                UserId = userId,
                ClientAccountId = clientAccount.ClientAccountId,
                TicketCategoryId = dto.TicketCategoryId,
                Title = dto.Title,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow
            };

             context.Tickets.Add(ticket);
            await  context.SaveChangesAsync();

            // 2️⃣ Add first TicketDetail with "Logged" status
            var loggedStatus = await  context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "Logged");

            if (loggedStatus == null)
                return BadRequest("Ticket status 'Logged' not found.");

            var detail = new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = userId,
                TicketStatusId = loggedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Ticket created by client, pending approval"
            };

             context.TicketDetails.Add(detail);
            await  context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.TicketId,
                Message = "Ticket created successfully and is waiting for approval."
            });
        }
        [HttpPost("{ticketId}/approve")]
        [Authorize(Roles = "Admin,Assignee,FirstLineSupport")]
        public async Task<IActionResult> ApproveTicket(int ticketId)
        {
            var ticket = await context.Tickets
                .Include(t => t.TicketDetails)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                return NotFound("Ticket not found.");

            // Get "Approved" status
            var assignedStatus = await context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "Approved");

            if (assignedStatus == null)
                return BadRequest("Ticket status 'Approved' not found.");

            // Add new TicketDetail for approval
            var detail = new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                TicketStatusId = assignedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Ticket approved by admin"
            };

             context.TicketDetails.Add(detail);
            await  context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.TicketId,
                Message = "Ticket approved and now active for processing."
            });
        }

       

        [HttpPost("{ticketId}/assign")]
        [Authorize(Roles = "Admin,Assignee,FirstLineSupport")]
        public async Task<IActionResult> AssignTicket(int ticketId, [FromBody] string assigneeUserId)
        {
            var ticket = await context.Tickets
                .Include(t => t.AssignedTo)
                .Include(t => t.TicketDetails)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                return NotFound("Ticket not found.");

            var assignedStatus = await context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "Assigned");

            if (assignedStatus == null)
                return BadRequest("Ticket status 'Assigned' not found.");

            // ✅ Assign user ID
            ticket.AssignToUserId = assigneeUserId;

            // Load user for description
            var user = await _userManager.FindByIdAsync(assigneeUserId);

            // Add TicketDetail
            var detail = new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = assigneeUserId,
                TicketStatusId = assignedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = user != null
                    ? $"Ticket assigned to {user.FirstName} {user.LastName}"
                    : $"Ticket assigned to user {assigneeUserId}"
            };

            context.TicketDetails.Add(detail);
            await context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.TicketId,
                AssignedTo = assigneeUserId,
                Message = "Ticket successfully assigned."
            });
        }

        [HttpGet("assignees")]
        public async Task<ActionResult<List<UserDto>>> GetAssignees()
        {
            var assignees = await _userManager.Users
                .Where(u => u.UserGroupId == 2)   // 2 = Assignee group
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email
                })
                .ToListAsync();

            return Ok(assignees);
        }

        public class UserDto
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }
        [Authorize(Roles = "Assignee,FirstLineSupport")]
        [HttpGet("assigned-to-me")]
        [ProducesResponseType(typeof(List<TicketReadOnlyDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TicketReadOnlyDto>>> GetTicketsAssignedToMe()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var tickets = await context.Tickets
                .Where(t => t.AssignToUserId == userId)
                .Select(t => new TicketReadOnlyDto
                {
                    TicketId = t.TicketId,
                    Title = t.Title,
                    CreatedAt = t.CreatedAt,
                    TicketCategoryId = t.TicketCategoryId
            
                })
                .ToListAsync();

            return Ok(tickets);
        }



        [HttpPost("{ticketId}/start")]
        [Authorize(Roles = "Assignee,FirstLineSupport")]
        public async Task<IActionResult> StartWork(int ticketId)
        {
            var ticket = await context.Tickets
                .Include(t => t.TicketDetails)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                return NotFound("Ticket not found.");

            // Only assigned user can start
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ticket.AssignToUserId != userId)
                return Forbid("You are not assigned to this ticket.");

            var inProgressStatus = await context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "In Progress");

            if (inProgressStatus == null)
                return BadRequest("Status 'In Progress' not found.");

            context.TicketDetails.Add(new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = userId,
                TicketStatusId = inProgressStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Work started on ticket"
            });

            await context.SaveChangesAsync();

            return Ok("Ticket is now in progress.");
        }

        [HttpPost("{ticketId}/close")]
        [Authorize(Roles = "Assignee,FirstLineSupport")]
        public async Task<IActionResult> CloseTicket(int ticketId)
        {
            var ticket = await context.Tickets
                .Include(t => t.TicketDetails)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                return NotFound("Ticket not found.");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // ✅ Only the assigned user can close the ticket
            if (ticket.AssignToUserId != userId)
                return Forbid("You are not assigned to this ticket.");

            // ✅ Get "Closed" status
            var closedStatus = await context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "Closed");

            if (closedStatus == null)
                return BadRequest("Ticket status 'Closed' not found.");

            // ✅ Add TicketDetail entry
            var detail = new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = userId,
                TicketStatusId = closedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Ticket closed by assignee"
            };

            context.TicketDetails.Add(detail);

            // ✅ Optional: mark ticket as resolved (if you use this field)
            ticket.ResolvedAt = DateTime.UtcNow;

            await context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.TicketId,
                Message = "Ticket successfully closed."
            });
        }

        [HttpPost("{ticketId}/reopen")]
        [Authorize(Roles = "Client,Admin,FirstLineSupport")]
        public async Task<IActionResult> ReopenTicket(int ticketId)
        {
            var ticket = await context.Tickets
                .Include(t => t.TicketDetails)
                .FirstOrDefaultAsync(t => t.TicketId == ticketId);

            if (ticket == null)
                return NotFound("Ticket not found.");

            var reopenedStatus = await context.TicketStatuses
                .FirstOrDefaultAsync(s => s.Status == "Reopened");

            if (reopenedStatus == null)
                return BadRequest("Ticket status 'Reopened' not found.");

            context.TicketDetails.Add(new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                TicketStatusId = reopenedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Ticket reopened"
            });

            // 🔁 Reset resolved date
            ticket.ResolvedAt = null;

            await context.SaveChangesAsync();

            return Ok("Ticket reopened successfully.");
        }





        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicket(int id, TicketUpdateDto dto)
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            mapper.Map(dto, ticket);
            await context.SaveChangesAsync();
            var readDto = mapper.Map<TicketUpdateDto>(ticket);
            return Ok(readDto);
        }

        //DELETE: api/Ticket/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketDetailsDto>> DeleteTicket(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();

            }
            context.Tickets.Remove(ticket);
            await context.SaveChangesAsync();
            //return NoContent();
            return Ok(mapper.Map<TicketDetailsDto>(ticket));
        }

    }

}
