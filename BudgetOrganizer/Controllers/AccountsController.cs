using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.AccountModel;
using AutoMapper;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly BudgetOrganizerDbContext _context;
		private readonly IMapper _mapper;

		public AccountsController(BudgetOrganizerDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

        //---------------------------------------------------------------------------
        //TODO: add autenthication
        //---------------------------------------------------------------------------

        // GET: api/Accounts
        [HttpGet]
		public async Task<ActionResult<IEnumerable<GetAccountDTO>>> GetAccounts()
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}

			var result = await _context.Accounts.ToListAsync();
			return Ok(_mapper.Map<List<Account>, List<GetAccountDTO>>(result));
		}

		// GET: api/Accounts/5
		[HttpGet]
		[Route("({id:guid})")]
		public async Task<ActionResult<GetAccountDTO>> GetAccount([FromRoute]Guid id)
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}

			var account = await _context.Accounts.FindAsync(id);

			if (account == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<GetAccountDTO>(account));
		}

		// POST: api/Accounts
		[HttpPost]
		public async Task<ActionResult<GetAccountDTO>> AddAccount(AddAccountDTO addAccountDTO)
		{
			if (_context.Accounts == null)
			{
				return Problem("Entity set 'BudgetOrganizerDbContext.Accounts'  is null.");
			}

            //---------------------------------------------------------------------------
            //TODO: Add model validation ex. if email or login is already taken etc.
            //---------------------------------------------------------------------------

            //Create and add to database new Account object

            //We use automapping (AccountMappingProfiles) to write one line of code instead of many:
            Account account = _mapper.Map<Account>(addAccountDTO);

			var role = await _context.Roles.FindAsync(addAccountDTO.RoleId);
			if(role == null)
			{
				return NotFound("Incorrect RoleId");
			}

			account.Role = role;
			//Account account = new Account()
			//{
			//	Id = Guid.NewGuid(),
			//	Login = addAccountDTO.Login,
			//	Email = addAccountDTO.Email,
			//	Password = addAccountDTO.Password
			//};

			await _context.AddAsync(account);
			await _context.SaveChangesAsync();

			return Ok(_mapper.Map<GetAccountDTO>(account));
		}

		// DELETE: api/Accounts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}

			var account = await _context.Accounts.FindAsync(id);
			if (account == null)
			{
				return NotFound();
			}

			_context.Accounts.Remove(account);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		//Auto generated code
		//private bool AccountExists(Guid id)
		//{
		//	return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
		//}
	}
}
