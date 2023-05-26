using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.ProfileModel;

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
        public ICollection<Profile> Users { get; set; }
    }
}
