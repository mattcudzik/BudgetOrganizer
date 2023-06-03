using BudgetOrganizer.Models.OperationModel;

namespace BudgetOrganizer.Services
{
    public interface IReportService
    {
        IQueryable<Operation> GetOpertaionsReport(Guid accountId, string? sortOrder, FilterOperationDTO? filterParam);
    }
}
