using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetOrganizer.Models.OperationModel
{
	public class AddOperationDTO
    {
		[ForeignKey("Category")]
		public Guid CategoryId { get; set; }
		public decimal Amount { get; set; }
        public DateTime? DateTime { get; set; }
    }
}
