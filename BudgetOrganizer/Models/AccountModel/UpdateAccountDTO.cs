using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class UpdateAccountDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
