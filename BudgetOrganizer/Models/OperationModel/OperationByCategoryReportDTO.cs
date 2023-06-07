using BudgetOrganizer.Models.CategoryModel;

namespace BudgetOrganizer.Models.OperationModel
{
    public class OperationByCategoryReportDTO
    {
        public GetCategoryDTO Category { get; set; }
        public int Sum { get; set; } = 0;
    }
}
