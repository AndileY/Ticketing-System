using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.ClientAccountCompanyAccess;

namespace TicketSystemWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientAccountCompanyAccessController : ControllerBase
    {
        private readonly TicketSystemDbContext _context;
        private readonly IMapper _mapper;

        public ClientAccountCompanyAccessController(TicketSystemDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/ClientAccountCompanyAccess
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientAccountCompanyAccessReadOnly>>> GetAll()
        {
            var accessList = await _context.ClientAccountCompanyAccesses
                .Join(_context.ClientAccounts,
                    access => access.ClientAccountId,
                    client => client.ClientAccountId,
                    (access, client) => new { access, client })
                .Join(_context.Companys  ,
                    combined => combined.access.CompanyId,
                    company => company.CompanyId,
                    (combined, company) => new ClientAccountCompanyAccessReadOnly
                    {
                        ClientAccountId = combined.client.ClientAccountId,
                        ClientFullName = combined.client.FirstName + " " + combined.client.LastName,
                        CompanyId = company.CompanyId,
                        CompanyName = company.CompanyName
                    })
                .ToListAsync();

            return Ok(accessList);
        }


        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<ClientAccountCompanyAccessReadOnly>>> GetAll([FromQuery] int? clientId = null)
        //{
        //    var query = _context.ClientAccountCompanyAccesses.AsQueryable();

        //    if (clientId.HasValue)
        //        query = query.Where(x => x.ClientAccountId == clientId.Value);

        //    var list = await query.ToListAsync();
        //    var result = _mapper.Map<List<ClientAccountCompanyAccessReadOnly>>(list);
        //    return Ok(result);
        //}

        // POST: api/ClientAccountCompanyAccess
        [HttpPost]
        public async Task<ActionResult> Add(ClientAccountCompanyAccessCreateDto dto)
        {
            var exists = await _context.ClientAccountCompanyAccesses
                .AnyAsync(x => x.ClientAccountId == dto.ClientAccountId && x.CompanyId == dto.CompanyId);

            if (exists)
                return Conflict("This access already exists.");

            var entity = _mapper.Map<ClientAccountCompanyAccess>(dto);
            _context.ClientAccountCompanyAccesses.Add(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }


        // DELETE: api/ClientAccountCompanyAccess?clientId=1&company=2
        [HttpDelete]
        public async Task<IActionResult> Delete(int clientId, int company)
        {
            var access = await _context.ClientAccountCompanyAccesses
                .FirstOrDefaultAsync(x => x.ClientAccountId == clientId && x.CompanyId == company);

            if (access == null)
                return NotFound();

            _context.ClientAccountCompanyAccesses.Remove(access);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }



}