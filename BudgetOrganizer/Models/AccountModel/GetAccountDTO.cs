using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class GetAccountDTO
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public Guid RoleId { get; set; }
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }
    }
}
