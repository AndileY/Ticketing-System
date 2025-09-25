using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ConsumebleItem;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumebleItemController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;

        public ConsumebleItemController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        //GET/api/ConsumebleItem
        [HttpGet]
        public async Task <ActionResult<IEnumerable<ConsumebleItemReadOnlyDto>>> GetAllConsumebleItem()
        {
            var item = await context.ConsumebleItems.ToListAsync();
            var items = mapper.Map<IEnumerable<ConsumebleItemReadOnlyDto>>(item);
            return Ok(items);
        }

        //GET/api/ConsumebleItem/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumebleItemDetailsDto>> GetConsumebleItem(int id)
        {
            var item = await context.ConsumebleItems.FindAsync(id);
            var items = mapper.Map<ConsumebleItemDetailsDto>(item);
            return Ok(items);
        }

        //POST/api/ConsumebleItem
        [HttpPost]
        public async Task<ActionResult> CreateConsumeble(ConsumebleItemCreateDto dto)
        {
            var item = mapper.Map<ConsumebleItem>(dto);
        
            context.ConsumebleItems.Add(item);
            await context.SaveChangesAsync();
            var readDto = mapper.Map<ConsumebleItemCreateDto>(item);
            return Ok(readDto);
        }

        //PUT/api/ConsumebleItem/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateConsumebleItems(int id,ConsumebleItemUpdateDto dto)
        {
            var item = await context.ConsumebleItems.FindAsync(id);
            if (item == null)
                return NotFound();
            mapper.Map(dto, item);
            await context.SaveChangesAsync();
            return Ok();

        }

        //DELETE/api/ConsumebleItem/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsumebleItemDetailsDto>> DeleteConsumebleItem(int id)
        {
            var ticket = await context.ConsumebleItems.FindAsync(id);
            if (ticket == null)
                return NotFound();
            context.ConsumebleItems.Remove(ticket);
            await context.SaveChangesAsync();
            return Ok(mapper.Map<ConsumebleItemDetailsDto>(ticket));
        }

    }
}
