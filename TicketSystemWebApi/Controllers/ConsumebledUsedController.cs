using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ConsumebledUsed;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsumebledUsedController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;
        public ConsumebledUsedController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        //GET/api/ConsumabledUsed
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ConsumebledUsedReadOnlyDto>>> GetAllConsumebledUsed()
        //{
        //    var consumebled = await context.ConsumebledUseds.ToListAsync();
        //    var consumeble = mapper.Map<IEnumerable<ConsumebledUsedReadOnlyDto>>(consumebled);
        //    return Ok(consumeble);
        //}
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumebledUsedReadOnlyDto>>> GetAllConsumebledUsed()
        {
            var consumebled = await context.ConsumebledUseds
                .Include(c => c.ConsumebleItem)
                .ToListAsync();

            var consumebleDtos = consumebled.Select(c => new ConsumebledUsedReadOnlyDto
            {
                Id = c.Id,
                ConsumebleUsedId = c.ConsumebleUsedId,
                Quantity = c.Quantity,
                Name = c.ConsumebleItem.Name // Assuming this is the name property
            });

            return Ok(consumebleDtos);
        }


        //GET/api/ConsumabledUsed/1
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumebledUsedDetailsDto>> GetConsumebledUsed(int id)
        {
            var consumebled = await context.ConsumebledUseds.FindAsync(id);
            if (consumebled == null)
                return NotFound();
            var consumeble = mapper.Map<ConsumebledUsedDetailsDto>(consumebled);
            return Ok(consumeble);


        }

        //POST/api/ConsumabledUsed
        [HttpPost]
        public async Task<ActionResult> CreateConsumebledUsed(ConsumebledUsedCreateDto dto)
        {
            var consumebled = mapper.Map<ConsumebledUsed>(dto);
            context.ConsumebledUseds.Add(consumebled);
            await context.SaveChangesAsync();

            var readDto = mapper.Map<ConsumebledUsedCreateDto>(consumebled);




            return Ok(readDto);



        }

        //PUT/api/ConsumabledUsed/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateConsumebledUsed(int id, ConsumebledUsedUpdateDto dto)
        {
            var consumebled = await context.ConsumebledUseds.FindAsync(id);
            if (consumebled == null)
            {
                return NotFound();
            }
            mapper.Map(dto, consumebled);
            await context.SaveChangesAsync();
            return Ok();
        }


        //DELETE/api/ConsumabledUsed/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ConsumebledUsedDetailsDto>> DeleteConsumebledUsed(int id)
        {
            var consumebled = await context.ConsumebledUseds.FindAsync(id);
            if (consumebled == null)
            {
                return NotFound();
            }
            context.Remove(consumebled);
            await context.SaveChangesAsync();
            return Ok(mapper.Map<ConsumebledUsedDetailsDto>(consumebled));
            
        }
    }
}
