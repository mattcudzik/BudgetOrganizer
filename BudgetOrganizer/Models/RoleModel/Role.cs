using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.RoleModel
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
