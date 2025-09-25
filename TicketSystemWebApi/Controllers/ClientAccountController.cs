using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ClientAccount;
using TicketSystemWebApi.Models.Ticket;

namespace TicketSystemWebApi.Controllers
{
    
        [Route("api/[controller]")]
        [ApiController]
        public class ClientAccountController : ControllerBase
        {
            private readonly TicketSystemDbContext _context;
            private readonly UserManager<User> _userManager;

            public ClientAccountController(TicketSystemDbContext context, UserManager<User> userManager)
            {
                _context = context;
                _userManager = userManager;
            }


            //Allow everyone to register
           [HttpPost]
           [AllowAnonymous]
           public async Task<ActionResult> CreateClientAccount(ClientAccountCreateDto dto)
           {
               var client = new ClientAccount
               {
                   FirstName = dto.FirstName,
                   LastName = dto.LastName,
                   CompanyId = dto.CompanyId,
                   Email = dto.Email,
                   Telephone = dto.Telephone,
                   Address = dto.Address,
                   QuickBooksUid = dto.QuickBooksUid,
                   Slaid = dto.Slaid,
                   IsApproved = false // default maybe
               };



               _context.ClientAccounts.Add(client);
               await _context.SaveChangesAsync();

               // Manual mapping for ReadOnlyDto
              var readDto = new ClientAccountReadOnlyDto
              {
                  ClientAccountId = client.ClientAccountId,
                  FirstName = client.FirstName,
                  LastName = client.LastName,
                  CompanyId = client.CompanyId,
                  Email = client.Email,
                  Telephone = client.Telephone,
                  Address = client.Address,
                  QuickBooksUid = client.QuickBooksUid,
                  Slaid = client.Slaid,
                  IsApproved = client.IsApproved,
                  CompanyName = null, // set if available
                  HasUserAccount = false // default
               };

             return Ok(readDto);
           }

            // Clients can view their own account
            // For Client - view ONLY their account
            [Authorize(Roles = "Client")]
            [HttpGet("me")]
            public async Task<IActionResult> GetMyClientAccount()
            {
                   var email = User.Identity?.Name;
                   if (string.IsNullOrEmpty(email)) return Unauthorized("No email found in token");

                   var client = await _context.ClientAccounts.FirstOrDefaultAsync(c => c.Email == email);
                  if (client == null) return NotFound("Client account not found");

                 var dto = new ClientAccountReadOnlyDto
                 {
                     ClientAccountId = client.ClientAccountId,
                     FirstName = client.FirstName,
                     LastName = client.LastName,
                     CompanyId = client.CompanyId,
                     Email = client.Email,
                     Telephone = client.Telephone,
                     Address = client.Address,
                     QuickBooksUid = client.QuickBooksUid,
                     Slaid = client.Slaid,
                     IsApproved = client.IsApproved,
                     CompanyName = null,
                     HasUserAccount = false

                 };

                  return Ok(dto);
            }
  
           // Only Admin can view client account by ID
            [HttpGet("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult<ClientAccountDetailsDto>> GetClientAccount(int id)
            {
                var client = await _context.ClientAccounts.FindAsync(id);
                if (client == null)
                    return NotFound();

                // Assuming you want to keep AutoMapper here for DetailsDto
                var dto = new ClientAccountDetailsDto
                {
                    ClientAccountId = client.ClientAccountId,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                    CompanyId = client.CompanyId,
                    Email = client.Email,
                    Telephone = client.Telephone,
                    Address = client.Address,
                    QuickBooksUid = client.QuickBooksUid,
                    Slaid = client.Slaid
                };

                return Ok(dto);
            }
            [Authorize(Roles = "Admin")]
            [HttpGet("all")]
            public async Task<IActionResult> GetAllClientAccounts()
            {
                var clients = await _context.ClientAccounts
                .Select(c => new ClientAccountReadOnlyDto
                {
                    ClientAccountId = c.ClientAccountId,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    CompanyId = c.CompanyId,
                    
                    Email = c.Email,
                    Telephone = c.Telephone,
                    Address = c.Address,
                    QuickBooksUid = c.QuickBooksUid,
                    Slaid = c.Slaid,
                    IsApproved = c.IsApproved,
                    CompanyName = null, // set if needed
                    HasUserAccount = false // compute if needed
                })
                .ToListAsync();

                return Ok(clients);
            }

       
             // Approve client and create user
             [HttpPost("approve/{id}")]
             [Authorize(Roles = "Admin")]
             public async Task<IActionResult> ApproveClient(int id)
             {
                 var clientAccount = await _context.ClientAccounts.FindAsync(id);
                  if (clientAccount == null)
                return NotFound("Client account not found");

                 if (clientAccount.IsApproved)
                return BadRequest("Already approved");

                clientAccount.IsApproved = true;

                 var temporaryPassword = "Temp@123!"; // You can randomize this

                var user = new User
                {
                   UserName = clientAccount.Email,
                   Email = clientAccount.Email,
                   FirstName = clientAccount.FirstName,
                   LastName = clientAccount.LastName,
                   UserGroupId = 4, // Assuming 4 = Client
                   IsTemporaryPassword = true,
                   TemporaryPasswordExpiry = DateTime.UtcNow.AddHours(24)
                };

                var result = await _userManager.CreateAsync(user, temporaryPassword);
                if (!result.Succeeded)
                {
                    var errorMessages = string.Join(", ", result.Errors.Select(e => e.Description));
                   return BadRequest($"User creation failed: {errorMessages}");
                }

                await _userManager.AddToRoleAsync(user, "Client");

                // ✅ Save the Identity UserId in ClientAccount
                clientAccount.UserId = user.Id;
                _context.ClientAccounts.Update(clientAccount);
                await _context.SaveChangesAsync();

                 return Ok(new
                {
                     Email = user.Email,
                     TemporaryPassword = temporaryPassword
                 });
             }
             // Only Admin can update client
            [HttpPut("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult> UpdateClientAccount(int id, ClientAccountUpdateDto dto)
            {
                 var client = await _context.ClientAccounts.FindAsync(id);
                 if (client == null)
                     return NotFound();

                 client.FirstName = dto.FirstName;
                 client.LastName = dto.LastName;
                 client.CompanyId = dto.CompanyId;
                 client.Email = dto.Email;
                 client.Telephone = dto.Telephone;
                 client.Address = dto.Address;
                 client.QuickBooksUid = dto.QuickBooksUid;
                 client.Slaid = dto.Slaid;

                 await _context.SaveChangesAsync();
                 return Ok();
            }




             // Only Admin can delete
            [HttpDelete("{id}")]
            [Authorize(Roles = "Admin")]
            public async Task<ActionResult> DeleteClientAccount(int id)
            {
                var client = await _context.ClientAccounts.FindAsync(id);
                if (client == null)
                    return NotFound();

                _context.ClientAccounts.Remove(client);
                await _context.SaveChangesAsync();

                return Ok();
            }
        }

        
}





















