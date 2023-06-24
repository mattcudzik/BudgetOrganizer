using BudgetOrganizer.Models.RoleModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class GetAccountDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }
        public Guid GroupId { get; set; }
    }
}
