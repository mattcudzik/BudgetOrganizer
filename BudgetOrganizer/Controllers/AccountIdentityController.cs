using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BudgetOrganizer.Models;
using BudgetOrganizer.Services;

namespace BudgetOrganizer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountIdentityController : ControllerBase
    {

        private readonly IAuthService _authService;
        public AccountIdentityController(
            IAuthService authService
        )
        {
            _authService = authService;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(AccountLoginData user)
        {
            if (await _authService.RegisterUser(user))
            {
                return Ok("Successfuly done");
            }
            return BadRequest("Something went worng");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(AccountLoginData user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (await _authService.Login(user))
            {
                var tokenString = _authService.GenerateTokenString(user);
                return Ok(tokenString);
            }
            return BadRequest();
        }

    }
}
