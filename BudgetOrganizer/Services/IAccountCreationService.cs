using BudgetOrganizer.Models.AccountModel;
using Microsoft.AspNetCore.Mvc;

namespace BudgetOrganizer.Services
{
    public interface IAccountCreationService
    {
        Task<Account> CreateNewAccount(AddAccountDTO addAccountDTO);
    }
}
