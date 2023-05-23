using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models
{
    public class Category
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Color { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
