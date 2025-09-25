using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.TicketDetail;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketDetailController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;

        public TicketDetailController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        //GET /api/TicketDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDetailReadOnlyDto>>> GetAllTicketDetail()
        {
            //var ticketdetails = await context.TicketDetails.ToListAsync();
            var ticketdetails = await context.TicketDetails
                 .Include(t => t.Ticket)
                 .ThenInclude(t => t.ClientAccount)
                  .Include(t => t.TicketStatus)
                 .ToListAsync();

            var dto = mapper.Map<IEnumerable<TicketDetailReadOnlyDto>>(ticketdetails);
            return Ok(dto);

   
        }

        //GET /api/TicketDetail/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDetailDto>> GetTicketDetail(int id)
        {
            //var ticketdetail = await context.TicketDetails.FindAsync(id);
            var ticketdetail = await context.TicketDetails
                  .Include(t => t.Ticket)
                  .ThenInclude(t => t.ClientAccount)
                   .Include(t => t.TicketStatus)
                  .FirstOrDefaultAsync(t => t.Id == id);

            if (ticketdetail == null)
                return NotFound();

            var dto = mapper.Map<TicketDetailDto>(ticketdetail);
            return Ok(dto);
        }

        //POST /api/TicketDetail
        [HttpPost]
        public async Task<ActionResult> CreateTicketDetail(TicketDetailCreateDto dto)
        {
            Console.WriteLine($"[DEBUG] TicketId = {dto.TicketId}"); // Check this is NOT 0
            Console.WriteLine($"[DEBUG] UserId = {dto.UserId}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketdetail = mapper.Map<TicketDetail>(dto);
            context.TicketDetails.Add(ticketdetail);
            await context.SaveChangesAsync();

            //var readDto = mapper.Map<TicketDetailDto>(ticketdetail);
            //var fullDetail = await context.TicketDetails
            //     .Include(t => t.Ticket)
            //     .ThenInclude(t => t.ClientAccount)
            //     .FirstOrDefaultAsync(t => t.Id == ticketdetail.Id);

            // Reload with related entities (include TicketStatus)
            var fullDetail = await context.TicketDetails
                .Include(t => t.Ticket)
                    .ThenInclude(t => t.ClientAccount)
                .Include(t => t.TicketStatus) // ✅ This fixes your issue
                .FirstOrDefaultAsync(t => t.Id == ticketdetail.Id);

            var readDto = mapper.Map<TicketDetailDto>(fullDetail);
            return Ok(readDto);

            //return CreatedAtAction(nameof(GetTicketDetail), new { id = ticketdetail.Id }, readDto);
        }

        //PUT /api/TicketDetail/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicketDetail(int id, TicketDetailUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticketdetail = await context.TicketDetails.FindAsync(id);
            if (ticketdetail == null)
                return NotFound();

            mapper.Map(dto, ticketdetail);
            await context.SaveChangesAsync();
            return Ok();

           

   

        }

        //DELETE /api/TicketDetail/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketDetailDto>> DeleteTicketDetail(int id)
        {
            var ticketdetail = await context.TicketDetails.FindAsync(id);
            if (ticketdetail == null)
                return NotFound();

            context.TicketDetails.Remove(ticketdetail);
            await context.SaveChangesAsync();
            return Ok(mapper.Map<TicketDetailDto>(ticketdetail));
        }
    }
}