using BudgetOrganizer.Models.OperationModel;

namespace BudgetOrganizer.Models
{
    public class OperationImage
    {
        public Guid Id { get; set; }
        public virtual Operation Operation { get; set; }
        public byte[] Image { get; set; }
    }
}
