using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class AddAccountDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }
        public Guid? GroupId { get; set; }
        public Guid? RoleId { get; set; }
    }
}
