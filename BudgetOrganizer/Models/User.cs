using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Role Role { get; set; }
        public int? PinNumber { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [Required]
        public Account Account { get; set; }
        public decimal? SpendingLimit { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
