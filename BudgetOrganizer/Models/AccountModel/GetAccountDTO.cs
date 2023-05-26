using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.ProfileModel;

namespace BudgetOrganizer.Models.AccountModel
{
    public class GetAccountDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
