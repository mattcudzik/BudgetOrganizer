using AutoMapper;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.AccountModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetOrganizer.Services
{

    public class AuthService : IAuthService
    {
        private readonly UserManager<Account> _userManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public AuthService(UserManager<Account> userManager, IConfiguration config, IMapper mapper)
        {
            _userManager = userManager;
            _config = config;
            _mapper = mapper;
        }

        /// <summary>
        /// Registers new user in database
        /// </summary>
        /// <returns></returns>
        public async Task<IdentityResult> RegisterUser(Account account, string password)
        {
            return await _userManager.CreateAsync(account, password);
        }
        /// <summary>
        /// Checks login data (throws exception if it wasn't successfull)
        /// </summary>
        /// <returns>account object if login was succesfull</returns>
        /// <exception cref="BadHttpRequestException"></exception>
        public async Task<Account> Login(LoginAccountDTO user)
        {
            var identityUser = await _userManager.FindByNameAsync(user.UserName);

            if (identityUser is null)
            {
                throw new BadHttpRequestException("User doesn't exists");
            }

            var result = await _userManager.CheckPasswordAsync(identityUser, user.Password);

            if (result)
            {
                return identityUser;
            }
            else
            {
                throw new BadHttpRequestException("Incorrect login or password");
            }
            
        }
        /// <summary>
        /// Generates token for user authorization
        /// </summary>
        /// <returns></returns>
        public string GenerateTokenString(Account user)
        {
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email,user.Email),
                //new Claim(ClaimTypes.Role,"Admin"),
                new Claim("id", user.Id.ToString())
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

        //public bool HasAccessToAccountData(Guid accountId, IEnumerable<Claim> claims)
        //{
        //    var claimId = claims.FirstOrDefault(new Claim("id", accountId.ToString())).Value;

        //    if (claimId == null)
        //        throw new BadHttpRequestException("Token describes account that doesn't exist");

        //    if (accountId.ToString() != claimId)
        //    {
        //        return false;
        //    }

        //    return true;
        //}

    }
}