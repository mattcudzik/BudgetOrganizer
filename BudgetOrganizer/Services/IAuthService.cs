using BudgetOrganizer.Models.AccountModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BudgetOrganizer.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(Account user);
        Task<Account> Login(LoginAccountDTO user);
        Task<IdentityResult> RegisterUser(Account user, string password);
        //bool HasAccessToAccountData(Guid accountId, IEnumerable<Claim> claims);
    }
}