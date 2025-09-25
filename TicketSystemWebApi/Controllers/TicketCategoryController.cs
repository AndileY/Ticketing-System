using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ConsumebledUsed;
using TicketSystemWebApi.Models.Ticket;
using TicketSystemWebApi.Models.TicketCategory;
using TicketSystemWebApi.Models.TicketDetail;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketCategoryController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;
        public TicketCategoryController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;

        }

        //GET/api/TicketCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketCategoryReadOnlyDto>>> GetAllTicketCategory()
        {
            var category = await context.TicketCategories.ToListAsync();
            var categories = mapper.Map<IEnumerable<TicketCategoryReadOnlyDto>>(category);
            return Ok(categories);
        }

        //GET/api/TicketCategory/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketCategoryDetailsDto>>GetTicketCategory(int id)
        {
            var category = await context.TicketCategories.FindAsync(id);
            if (category == null)
                return NotFound();
            var categories = mapper.Map<TicketCategoryDetailsDto>(category);
            return Ok(categories);



        }

        //POST/api/TicketCategory/{id}
        [HttpPost]
        public async Task<ActionResult> CreateTicketCategory(TicketCategoryCreateDto dto)
        {
            var category = mapper.Map<TicketCategory>(dto);
          
            context.TicketCategories.Add(category);
            await context.SaveChangesAsync();
            var readDto = mapper.Map<TicketCategoryCreateDto>(category);

            return CreatedAtAction(nameof(GetTicketCategory), new { id = category.TicketCategoryId }, readDto);






        }

        //PUT/api/TicketCategory/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateTicketCategory(int id, TicketCategoryUpdateDto dto)
        {
            var category = await context.TicketCategories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            mapper.Map(dto, category);
            await context.SaveChangesAsync();
            return NoContent();

        }


        //DELETE/api/TicketCategory/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TicketCategoryDetailsDto>>DeleteTicketCategory(int id)
        {
            var category = await context.TicketCategories.FindAsync(id);
            if (category == null)
                return NotFound();
            context.TicketCategories.Remove(category);
            await context.SaveChangesAsync();
            //return NoContent();
            return Ok(mapper.Map<TicketCategoryDetailsDto>(category));

        }

    }
}
