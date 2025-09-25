using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        public TicketController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketReadOnlyDto>>> GetAllTicket()
        {
            var ticket = await context.Tickets.ToListAsync();
            var tickets = mapper.Map<IEnumerable<TicketReadOnlyDto>>(ticket);
            return Ok(tickets);
        }

        // GET: api/Ticket/1
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDetailsDto>> GetTicket(int id)
        {
            var ticket = await context.Tickets.FindAsync(id);
            var tickets = mapper.Map<TicketDetailsDto>(ticket);
            return Ok(tickets);
        }



        [Authorize(Roles = "Client")]
        [HttpPost("create")]
        public async Task<IActionResult> CreateTicket(TicketCreateDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User not found.");

            // 1️⃣ Get client account
            var clientAccount = await context.ClientAccounts
              .FirstOrDefaultAsync(c => c.UserId == userId);

            if (clientAccount == null)
                return BadRequest("Client account not linked to your user.");

            // 2️⃣ Create ticket
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

            // 3️⃣ Get "Logged" status from TicketStatus
            var loggedStatus = await context.TicketStatuses
              .FirstOrDefaultAsync(s => s.Status == "Logged");

            if (loggedStatus == null)
                return BadRequest("Ticket status 'Logged' not found.");

            // 4️⃣ Create first TicketDetail
            var detail = new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = userId,
                TicketStatusId = loggedStatus.TicketStatusId,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                Description = "Ticket created by client"
            };

             context.TicketDetails.Add(detail);
            await  context.SaveChangesAsync();

            return Ok(new
            {
                TicketId = ticket.TicketId,
                Message = "Ticket created successfully and logged."
            });
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
