using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.ProfileModel;

namespace BudgetOrganizer.Models
{
    public class Operation
    {
        [Required]
        public Guid Id { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        [Required]
        public Profile User { get; set; }
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [ForeignKey("OperationImage")]
        public Guid ImageId { get; set; }
        public virtual OperationImage Image { get; set; }

    }
}
