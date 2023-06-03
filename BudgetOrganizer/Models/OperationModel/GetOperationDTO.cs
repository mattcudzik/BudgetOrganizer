using BudgetOrganizer.Models.CategoryModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetOrganizer.Models.OperationModel
{
    public class GetOperationDTO
    {
        public Guid Id { get; set; }
        [ForeignKey("Category")]
        //public Guid CategoryId { get; set; }
        public GetCategoryDTO Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("OperationImage")]
        public Guid? ImageId { get; set; }
        //public virtual OperationImage? Image { get; set; }

    }
}
