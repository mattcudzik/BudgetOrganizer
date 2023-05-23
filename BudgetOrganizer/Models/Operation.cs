using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models
{
    public class Operation
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public virtual OperationImage Image { get; set; }

    }
}
