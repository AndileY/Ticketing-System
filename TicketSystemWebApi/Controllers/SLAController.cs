using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ClientAccount;
using TicketSystemWebApi.Models.Sla;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SLAController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;
        public SLAController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }
        // GET: api/Sla
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SlaReadOnlyDto>>> GetAllSLA()
        {
            var agreement = await context.Slas.ToListAsync();
            var aggreements = mapper.Map<IEnumerable<SlaReadOnlyDto>>(agreement);
            return Ok(aggreements);
           
        }
        // GET: api/Sla/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SlaDetailsDto>>GetSla(int id)
        {
            var aggreement = await context.Slas.FindAsync(id);
            if (aggreement == null)
            {
                return NotFound();
            }
            var aggreements = mapper.Map<SlaDetailsDto>(aggreement);
            return Ok(aggreements);
           

            
        }
        // POST: api/Sla
        [HttpPost]
        public async Task<ActionResult>CreateSla([FromBody] SlaCreateDto dto)
        {
            var aggrement = mapper.Map<Sla>(dto);
            context.Slas.Add(aggrement);
            await context.SaveChangesAsync();

            var readDto = mapper.Map<SlaReadOnlyDto>(aggrement);
            //return CreatedAtAction(nameof(GetSla), new { id = aggrement.Slaid }, readDto);
            return Ok(readDto);




        }
        // PUT: api/Sla/5
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateSla(int id,SlaUpdateDto dto)
        {
            var aggreement = await context.Slas.FindAsync(id);
            if (aggreement == null)
                return NotFound();
            mapper.Map(dto, aggreement);
            await context.SaveChangesAsync();
            //return NoContent();
            return Ok();

        }
        // DELETE: api/Sla/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SlaDetailsDto>>DeleteSlas(int id)
        {
            var aggreements = await context.Slas.FindAsync(id);
            if(aggreements == null)
            {
                return NotFound();
            }
            context.Remove(aggreements);
            await context.SaveChangesAsync();
            //return NoContent();

            // This is what fixes your Blazor 200/null issue:
            return Ok(new { Message = "Deleted successfully" });






        }
    }
}
 