using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly BudgetOrganizerDbContext _context;

		public AccountsController(BudgetOrganizerDbContext context)
		{
			_context = context;
		}

		//TODO add autenthication


		// GET: api/Accounts
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}

			return Ok(await _context.Accounts.ToListAsync());
		}

		// GET: api/Accounts/5
		[HttpGet]
		[Route("({id:guid})")]
		public async Task<ActionResult<Account>> GetAccount([FromRoute]Guid id)
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

			return Ok(account);
		}

		// PUT: api/Accounts/5
		[HttpPut("{id:guid}")]
		public async Task<IActionResult> PutAccount([FromRoute] Guid id, UpdateAccountDTO updateAccountDTO)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}

			var account = await _context.Accounts.FindAsync(id);

			if(account == null)
			{
				return NotFound();
			}

			account.Email = updateAccountDTO.Email;
			account.Login = updateAccountDTO.Email;
			account.Password = updateAccountDTO.Password;

			return NoContent();
		}

		// POST: api/Accounts
		[HttpPost]
		public async Task<ActionResult<Account>> PostAccount(AddAccountDTO addAccountDTO)
		{
			if (_context.Accounts == null)
			{
				return Problem("Entity set 'BudgetOrganizerDbContext.Accounts'  is null.");
			}

			//Checks if addAccountRequest is valid according to annotations ex. [Required]
			if (!ModelState.IsValid)
			{
				return BadRequest();
			}


			//TODO: Add model validation ex. if email is formatted like: example@example.com


			//Create and add to database new account object
			Account account = new Account()
			{
				Id = Guid.NewGuid(),
				Login = addAccountDTO.Login,
				Email = addAccountDTO.Email,
				Password = addAccountDTO.Password
			};

			await _context.AddAsync(account);
			await _context.SaveChangesAsync();

			//return CreatedAtAction("GetAccount", new { id = account.Id }, account);
			return Ok(account);
		}

		// DELETE: api/Accounts/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccount(Guid id)
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

		private bool AccountExists(Guid id)
		{
			return (_context.Accounts?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
