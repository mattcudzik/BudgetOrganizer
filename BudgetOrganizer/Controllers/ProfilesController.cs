using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.ProfileModel;
using Microsoft.AspNetCore.Authorization;

namespace BudgetOrganizer.Controllers
{
	[Route("api/Accounts")]
	[ApiController]
    [Authorize]
    public class ProfilesController : ControllerBase
	{
		private readonly BudgetOrganizerDbContext _context;
        private readonly AutoMapper.IMapper _mapper;

        public ProfilesController(BudgetOrganizerDbContext context, AutoMapper.IMapper mapper)
		{
			_context = context;
            _mapper = mapper;
        }

		[HttpGet]
		[Route("{accountId:guid}/[controller]")]
		public async Task<ActionResult<IEnumerable<GetProfileDTO>>> GetProfiles([FromRoute] string accountId)
		{
			if (_context.Profiles == null)
			{
				return NotFound();
			}

			var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }

            var profiles = _context.Profiles.Where(p => p.AccountId == accountId).ToList();
            
			return Ok(_mapper.Map<List<Profile>,List<GetProfileDTO>>(profiles));
		}

        [HttpPost]
        [Route("{accountId:guid}/[controller]")]
        public async Task<ActionResult<GetProfileDTO>> PostProfile([FromRoute] Guid accountId, AddProfileDTO addProfile)
        {
            if (_context.Profiles == null)
            {
                return Problem("Entity set 'BudgetOrganizerDbContext.Profiles'  is null.");
            }

            var account = await _context.Accounts.FindAsync(accountId);

            if (account == null)
            {
                return NotFound();
            }

            Profile profile = _mapper.Map<Profile>(addProfile);

            profile.AccountId = account.Id;
            profile.Account = account;

            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<GetProfileDTO>(profile));

            //return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        }


        //    // GET: api/Profiles/5
        //    [HttpGet("{id}")]
        //    public async Task<ActionResult<Profile>> GetProfile(Guid id)
        //    {
        //      if (_context.Profiles == null)
        //      {
        //          return NotFound();
        //      }
        //        var profile = await _context.Profiles.FindAsync(id);

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
        //      if (_context.Profiles == null)
        //      {
        //          return Problem("Entity set 'BudgetOrganizerDbContext.Profiles'  is null.");
        //      }
        //        _context.Profiles.Add(profile);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction("GetProfile", new { id = profile.Id }, profile);
        //    }

        //    // DELETE: api/Profiles/5
        //    [HttpDelete("{id}")]
        //    public async Task<IActionResult> DeleteProfile(Guid id)
        //    {
        //        if (_context.Profiles == null)
        //        {
        //            return NotFound();
        //        }
        //        var profile = await _context.Profiles.FindAsync(id);
        //        if (profile == null)
        //        {
        //            return NotFound();
        //        }

        //        _context.Profiles.Remove(profile);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }

        //    private bool ProfileExists(Guid id)
        //    {
        //        return (_context.Profiles?.Any(e => e.Id == id)).GetValueOrDefault();
        //    }
    }
}
