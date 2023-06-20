using BudgetOrganizer.Models.OperationModel;
using Microsoft.AspNetCore.Mvc;

namespace BudgetOrganizer.Services
{
    public interface IReportService
    {
        IQueryable<Operation>? GetOpertaionsReport(Guid accountId, string? sortOrder, FilterOperationDTO? filterParam);
        Task<List<OperationByCategoryReportDTO>> GetOpertaionsCategoryReport(Guid accountId, bool positive);
    }
}
