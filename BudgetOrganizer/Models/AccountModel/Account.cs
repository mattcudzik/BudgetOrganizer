using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.GroupModel;
using BudgetOrganizer.Models.OperationModel;
using BudgetOrganizer.Models.RoleModel;
using Microsoft.AspNetCore.Identity;

namespace BudgetOrganizer.Models.AccountModel
{
    public class Account : IdentityUser<Guid>
    {
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }

        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        [ForeignKey("Group")]
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        public ICollection<Operation> Operations { get; } = new List<Operation>();
        public ICollection<Category> Categories { get; } = new List<Category>();
    }
}
