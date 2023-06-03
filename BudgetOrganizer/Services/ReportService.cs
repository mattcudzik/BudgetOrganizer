using BudgetOrganizer.Models;
using BudgetOrganizer.Models.OperationModel;
using Microsoft.EntityFrameworkCore;

namespace BudgetOrganizer.Services
{
    public class ReportService : IReportService
    {
        private readonly BudgetOrganizerDbContext _context;
        public ReportService(BudgetOrganizerDbContext context)
        {
            _context = context;
        }

        public IQueryable<Operation> GetOpertaionsReport(Guid accountId, string? sortOrder, FilterOperationDTO? filterParam)
        {
            if (_context.Operations == null)
                return null;

            var operations = _context.Operations.Where(operation => operation.AccountId == accountId);

            //filtering retrived data
            if (filterParam != null)
            {
                if (filterParam.DateFrom != null)
                {
                    operations = operations.Where(operation => operation.DateTime >= filterParam.DateFrom);
                }

                if (filterParam.DateTo != null)
                {
                    operations = operations.Where(operation => operation.DateTime <= filterParam.DateTo);
                }

                if (filterParam.AmountFrom != null)
                {
                    operations = operations.Where(operation => operation.Amount >= filterParam.AmountFrom);
                }

                if (filterParam.AmountTo != null)
                {
                    operations = operations.Where(operation => operation.Amount <= filterParam.AmountTo);
                }

                if (filterParam.CategoriesId != null)
                {
                    operations = operations.Where(operation => !filterParam.CategoriesId.Contains(operation.CategoryId));
                }
            }

            //sorting retived data
            switch (sortOrder)
            {
                case "date_asc":
                    operations = operations.OrderBy(o => o.DateTime);
                    break;
                case "amount_desc":
                    operations = operations.OrderByDescending(o => o.Account);
                    break;
                case "amount_asc":
                    operations = operations.OrderBy(o => o.Account);
                    break;
                default:
                    operations = operations.OrderByDescending(o => o.DateTime);
                    break;
            }

            return operations;
        }
    }
}
