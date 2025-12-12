

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.TicketStatus;

[Route("api/[controller]")]
[ApiController]
public class TicketStatusController : ControllerBase
{
    private readonly TicketSystemDbContext context;
    private readonly IMapper mapper;

    public TicketStatusController(TicketSystemDbContext context, IMapper mapper)
    {
        this.mapper = mapper;
        this.context = context;
    }

    // GET: api/TicketStatus
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TicketStatusReadOnly>>> GetAllTicketStatus()
    {
        var status = await context.TicketStatuses.ToListAsync();
        var statuses = mapper.Map<IEnumerable<TicketStatusReadOnly>>(status);
        return Ok(statuses);
    }

    // GET: api/TicketStatus/1
    [HttpGet("{id}")]
    public async Task<ActionResult<TicketStatusDetailsDto>> GetTicketStatus(int id)
    {
        var status = await context.TicketStatuses.FindAsync(id);
        if (status == null)
            return NotFound();

        var dto = mapper.Map<TicketStatusDetailsDto>(status);
        return Ok(dto);
    }

    // POST: api/TicketStatus
    [HttpPost]
    public async Task<ActionResult<TicketStatusDetailsDto>> CreateTicketStatus([FromBody] TicketStatusCreateDto dto)
    {
        var status = mapper.Map<TicketStatus>(dto);

        // ✅ Explicitly set CreatedAt
        status.CreatedAt = DateTime.UtcNow;

        context.TicketStatuses.Add(status);
        await context.SaveChangesAsync();

        var result = mapper.Map<TicketStatusDetailsDto>(status);
        return Ok(result);
  
    }


    // PUT: api/TicketStatus/1
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTicket(int id, TicketStatusUpdateDto dto)
    {
        var status = await context.TicketStatuses.FindAsync(id);
        if (status == null)
            return NotFound();

        mapper.Map(dto, status);
        context.Entry(status).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return Ok();
    }

    // DELETE: api/TicketStatus/1
    [HttpDelete("{id}")]
    public async Task<ActionResult<TicketStatusDetailsDto>> DeleteTicketStatus(int id)
    {
        var status = await context.TicketStatuses.FindAsync(id);
        if (status == null)
            return NotFound();

        context.TicketStatuses.Remove(status);
        await context.SaveChangesAsync();
        return Ok(mapper.Map<TicketStatusDetailsDto>(status));
     
    }
}
