namespace BudgetOrganizer.Models
{
    public class OperationImage
    {
        public virtual Operation Operation { get; set; }
        public byte[] Image { get; set; }
    }
}
