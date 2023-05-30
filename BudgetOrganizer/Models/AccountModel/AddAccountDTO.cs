using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class AddAccountDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Guid? RoleId { get; set; }
    }
}
