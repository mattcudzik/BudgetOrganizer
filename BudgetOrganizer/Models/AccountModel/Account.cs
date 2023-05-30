using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.OperationModel;
using BudgetOrganizer.Models.ProfileModel;
using Microsoft.AspNetCore.Identity;

namespace BudgetOrganizer.Models.AccountModel
{
    public class Account : IdentityUser
    {
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Operation> Operations { get; } = new List<Operation>();
        public ICollection<Category> Categories { get; } = new List<Category>();
    }
}
