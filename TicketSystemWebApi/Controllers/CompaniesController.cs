using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.Companies;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;

        public CompaniesController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        //GET/api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompaniesReadOnly>>> GetAllCompanies()
        {
            var company = await context.Companys.ToListAsync();
            var companie = mapper.Map<IEnumerable<CompaniesReadOnly>>(company);
            return Ok(companie);
        }

        //GET/api/Companies/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CompaniesDetailsDto>> GetCompanies(int id)
        {
            var company = await context.Companys.FindAsync(id);
            var companie = mapper.Map<CompaniesDetailsDto>(company);
            return Ok(companie);
        }

        //POST/api/Companies
        [HttpPost]
        public async Task<ActionResult> CreateCompanies(CompaniesCreateDto dto)
        {
            var company = mapper.Map<Company>(dto);
            if (company == null)
                return NotFound();
            context.Companys.Add(company);
            await context.SaveChangesAsync();
            var readDto = mapper.Map<CompaniesCreateDto>(company);
            return CreatedAtAction(nameof(GetCompanies), new { id = company.CompanyId }, readDto);


        }

        //PUT/api/Companies/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCompanies(int id, CompaniesUpdateDto dto)
        {
            var company = await context.Companys.FindAsync(id);
            if (company == null)
                return NotFound();
            mapper.Map(dto, company);
            await context.SaveChangesAsync();
            return Ok();
        }

        //DELETE/api/Companies/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompaniesDetailsDto>> DeleteCompanies(int id)
        {
            var company = await context.Companys.FindAsync(id);
            if (company == null)
                return NotFound();

            context.Companys.Remove(company);
            await context.SaveChangesAsync();
            var dto = mapper.Map<CompaniesDetailsDto>(company);
            return Ok(dto);
        }
    }
}
