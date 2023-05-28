using BudgetOrganizer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetOrganizer.Services
{

        public class AuthService : IAuthService
        {
            private readonly UserManager<AccountIdentity> _userManager;
            private readonly IConfiguration _config;

            public AuthService(UserManager<AccountIdentity> userManager, IConfiguration config)
            {
                _userManager = userManager;
                _config = config;
            }

            public async Task<bool> RegisterUser(AccountLoginData user)
            {
                var identityUser = new AccountIdentity
                {
                    UserName = user.UserName,
                    Email = user.Email
                };

                var result = await _userManager.CreateAsync(identityUser, user.Password);
                return result.Succeeded;
            }

            public async Task<bool> Login(AccountLoginData user)
            {
                var identityUser = await _userManager.FindByEmailAsync(user.Email);
                if (identityUser is null)
                {
                    return false;
                }

                return await _userManager.CheckPasswordAsync(identityUser, user.Password);
            }

            public string GenerateTokenString(AccountLoginData user)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.UserName),
                new Claim(ClaimTypes.Role,"Admin"),
            };

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Jwt:Key").Value));

                var signingCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    issuer: _config.GetSection("Jwt:Issuer").Value,
                    audience: _config.GetSection("Jwt:Audience").Value,
                    signingCredentials: signingCred);

                string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return tokenString;
            }
        }
}
