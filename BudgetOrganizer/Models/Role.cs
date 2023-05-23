using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models
{
    public class Role
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
