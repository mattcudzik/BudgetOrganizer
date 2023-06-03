using Microsoft.AspNetCore.Mvc;

namespace BudgetOrganizer.Models.OperationModel
{
    [BindProperties]
    public class FilterOperationDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set;}
        public decimal? AmountFrom { get; set; }
        public decimal? AmountTo { get;set; }
        public Guid[]? CategoriesId { get; set; }
    }
}
