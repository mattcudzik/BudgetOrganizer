using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models.RoleModel
{
    public class Role
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
