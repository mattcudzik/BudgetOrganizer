using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.ProfileModel;

namespace BudgetOrganizer.Models.OperationModel
{
    public class GetOperationDTO
    {
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }
        //public Category Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("OperationImage")]
        public Guid? ImageId { get; set; }
        public virtual OperationImage? Image { get; set; }

    }
}
