using BudgetOrganizer.Models.RoleModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.AccountModel
{
    public class GetGroupAccountDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public RoleDTO Role { get; set; }
    }
}
