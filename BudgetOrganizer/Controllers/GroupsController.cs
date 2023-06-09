using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.GroupModel;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;

        public GroupsController(BudgetOrganizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Group>>> GetGroups()
        {
          if (_context.Groups == null)
          {
              return NotFound();
          }
            return await _context.Groups.ToListAsync();
        }

        //// GET: api/Groups/5
        //[Authorize]
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Group>> GetGroup(Guid id)
        //{
        //  if (_context.Groups == null)
        //  {
        //      return NotFound();
        //  }
        //    var @group = await _context.Groups.FindAsync(id);

        //    if (@group == null)
        //    {
        //        return NotFound();
        //    }

        //    return @group;
        //}

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<GroupDTO>> GetCurrentAccountGroup()
        {
            if (_context.Groups == null)
            {
                return NotFound();
            }

            var claim = HttpContext.User.FindFirst("id");
            if (claim == null)
                return StatusCode(500);

            var accountId = new Guid(claim.Value);
            var account = await _context.Accounts.Where(e => e.Id == accountId).Include(c => c.Group).ThenInclude(o => o.Accounts).Include(o=>o.Role).FirstOrDefaultAsync();

            if (account == null)
                return StatusCode(500);

            var group = account.Group;
            if (group == null)
                return Problem("Group is empty");

            return Ok(_mapper.Map<GroupDTO>(group));

        }
    }
}
