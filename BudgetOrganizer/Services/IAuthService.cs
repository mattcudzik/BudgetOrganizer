using BudgetOrganizer.Models;

namespace BudgetOrganizer.Services
{
    public interface IAuthService
    {
        string GenerateTokenString(AccountLoginData user);
        Task<bool> Login(AccountLoginData user);
        Task<bool> RegisterUser(AccountLoginData user);
    }
}
