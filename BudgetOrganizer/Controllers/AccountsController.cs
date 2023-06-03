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
using BudgetOrganizer.Services;
using BudgetOrganizer.Models.RoleModel;
using BudgetOrganizer.Models.GroupModel;
using System.Web.Http.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NuGet.Common;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly BudgetOrganizerDbContext _context;
		private readonly IMapper _mapper;
        private readonly IAuthService _authService;
		private readonly IAccountCreationService _accountCreationService;
        public AccountsController(
			BudgetOrganizerDbContext context, IMapper mapper,
			IAuthService authService, IAccountCreationService accountCreationService)
		{
			_context = context;
			_mapper = mapper;
			_authService = authService;
            _accountCreationService = accountCreationService;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(AddAccountDTO addAccountDTO)
        {
            if (_context.Accounts == null)
            {
                return Problem("Entity set 'BudgetOrganizerDbContext.Accounts'  is null.");
            }

			Account? account;

			try
			{
				account = await _accountCreationService.CreateNewAccount(addAccountDTO);
            }
			catch(BadHttpRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			
			if(account == null)
				return StatusCode(500);

            var result = await _authService.RegisterUser(account, addAccountDTO.Password);

            if (result.Succeeded)
            {
                return Ok("Successfuly done");
            }

            return BadRequest(result.Errors);
        }
		/// <summary>
		/// Login request
		/// </summary>
		/// <param name="loginAccountDTO"></param>
		/// <returns>User token or error</returns>
		[HttpPost("Login")]
		public async Task<IActionResult> Login(LoginAccountDTO loginAccountDTO)
		{
			if (!ModelState.IsValid)
            {
                return BadRequest();
			}
			try
			{
				var account = await _authService.Login(loginAccountDTO);
                var tokenString = _authService.GenerateTokenString(account);
				return Ok(tokenString);
            }
			catch (BadHttpRequestException ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//TODO: allow only admin
		// GET: api/Accounts
		[Authorize]
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
		[Authorize]
		[HttpGet]
		[Route("({id:guid})")]
		public async Task<ActionResult<GetAccountDTO>> GetAccount([FromRoute]Guid id)
		{
			if (_context.Accounts == null)
			{
				return NotFound("Account doesn't exists");
			}

			try
			{
                if (!_authService.HasAccessToAccountData(id, HttpContext.User.Claims))
                    return Unauthorized("You don't have access to that account");
            }
			catch (BadHttpRequestException ex)
			{
				return BadRequest(ex.Message);
			}

            var account = await _context.Accounts.FindAsync(id);

			if (account == null)
			{
				return NotFound();
			}

			return Ok(_mapper.Map<GetAccountDTO>(account));
		}

		[Authorize]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
		{
			if (_context.Accounts == null)
			{
				return NotFound();
			}

            try
            {
                if (!_authService.HasAccessToAccountData(id, HttpContext.User.Claims))
                    return Unauthorized("You don't have access to that account");
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
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
	}
}
