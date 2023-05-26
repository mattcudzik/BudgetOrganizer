using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.ProfileModel;
using Microsoft.Identity.Client;

namespace BudgetOrganizer.Controllers
{
    [Route("api/{accountId}/[controller]")]
    [ApiController]
    public class ProfilesController : ControllerBase
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly int _accountId;

        public ProfilesController(BudgetOrganizerDbContext context, int accountId)
        {
            _context = context;
            _accountId = accountId;
        }

        // GET: api/Profiles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound(_accountId);
          }
            return await _context.Users.ToListAsync();
        }

    //    // GET: api/Profiles/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<Profile>> GetProfile(Guid id)
    //    {
    //      if (_context.Users == null)
    //      {
    //          return NotFound();
    //      }
    //        var profile = await _context.Users.FindAsync(id);

    //        if (profile == null)
    //        {
    //            return NotFound();
    //        }

    //        return profile;
    //    }

    //    // PUT: api/Profiles/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutProfile(Guid id, Profile profile)
    //    {
    //        if (id != profile.Id)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(profile).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ProfileExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/Profiles
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<Profile>> PostProfile(Profile profile)
    //    {
    //      if (_context.Users == null)
    //      {
    //          return Problem("Entity set 'BudgetOrganizerDbContext.Users'  is null.");
    //      }
    //        _context.Users.Add(profile);
    //        await _context.SaveChangesAsync();

    //        return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
    //    }

    //    // DELETE: api/Profiles/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteProfile(Guid id)
    //    {
    //        if (_context.Users == null)
    //        {
    //            return NotFound();
    //        }
    //        var profile = await _context.Users.FindAsync(id);
    //        if (profile == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.Users.Remove(profile);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool ProfileExists(Guid id)
    //    {
    //        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    //    }
    }
}
