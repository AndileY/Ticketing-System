using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystemWebApi.Data;
using TicketSystemWebApi.Models.UserGroup;

namespace TicketSystemWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly TicketSystemDbContext context;
        public UserGroupsController(IMapper mapper, TicketSystemDbContext context)
        {
            this.mapper = mapper;
            this.context = context;

        }
        //GET/api/UserGroup
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGroupReadOnlyDto>>> GetAllUserGroups()
        {
            var group = await context.UserGroups.ToListAsync();
            var groups = mapper.Map<IEnumerable<UserGroupReadOnlyDto>>(group);
            return Ok(groups);

        }

        //GET/api/UserGroup/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UserGroupDetailDto>> GetUserGroups(int id)
        {
            var group = await context.UserGroups.FindAsync(id);
            if (group == null)
                return NotFound();
            var groups = mapper.Map<UserGroupDetailDto>(group);
            return Ok(groups);
        }

        //POST/api/UserGroup
        [HttpPost]
        public async Task<ActionResult>CreateUserGroups(UserGroupCreateDto dto)
        {
            var group = mapper.Map<UserGroup>(dto);
            if (group == null)
                return NotFound();
            context.UserGroups.Add(group);
            await context.SaveChangesAsync();
            var readDto = mapper.Map<UserGroupCreateDto>(group);
            return Ok(readDto);
        }

        //PUT/api/UserGroup
        [HttpPut]
        public async Task<ActionResult>UpdateUserGroup(int id, UserGroupUpdateDto dto)
        {
            var group = await context.UserGroups.FindAsync(id);
            if(group == null)
                return NotFound();
            mapper.Map(dto, group);
            await context.SaveChangesAsync();

            return Ok();
      

        }

        //DELETE/api/UserGroup
        [HttpDelete("{id}")]
        public  async Task <ActionResult<UserGroupDetailDto>>DeleteUserGroups(int id)
        {
            var group = await context.UserGroups.FindAsync(id);
            if (group == null)
                return NotFound();
            context.UserGroups.Remove(group);
            await context.SaveChangesAsync();
            return Ok(mapper.Map<UserGroupDetailDto>(group));

        }
        
        




    }
}
