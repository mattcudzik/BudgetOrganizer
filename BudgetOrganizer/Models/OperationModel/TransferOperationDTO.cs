using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetOrganizer.Models.OperationModel
{
	public class TransferOperationDTO
    {
        [Range(1,100000)]
		public decimal Amount { get; set; }
        public Guid DestinationAccount { get; set; }
    }
}
