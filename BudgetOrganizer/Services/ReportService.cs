using AutoMapper;
using BudgetOrganizer.Models;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.OperationModel;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BudgetOrganizer.Services
{
    public class ReportService : IReportService
    {
        private readonly BudgetOrganizerDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(BudgetOrganizerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<Operation> GetOpertaionsReport(Guid accountId, string? sortOrder, FilterOperationDTO? filterParam)
        {
            if (_context.Operations == null)
                throw new Exception("Database error");

            var operations = _context.Operations.Where(operation => operation.AccountId == accountId).Include(o=>o.Category).AsQueryable();

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

                if (filterParam.OnlyPositive != null)
                {
                    if ((bool) filterParam.OnlyPositive)
                        operations = operations.Where(operation => operation.Amount > 0);
                    else
                        operations = operations.Where(operation => operation.Amount < 0);
                }

            }

            //sorting retived data
            switch (sortOrder)
            {
                case "date_asc":
                    operations = operations.OrderBy(o => o.DateTime).ThenBy(o => o.CategoryId);
                    break;
                case "amount_desc":
                    operations = operations.OrderByDescending(o => o.Amount).ThenBy(o => o.CategoryId);
                    break;
                case "amount_asc":
                    operations = operations.OrderBy(o => o.Amount).ThenBy(o => o.CategoryId);
                    break;
                default:
                    operations = operations.OrderByDescending(o => o.DateTime).ThenBy(o => o.CategoryId);
                    break;
            }

            return operations;
        }

        public async Task<List<OperationByCategoryReportDTO>> GetOpertaionsCategoryReport(Guid accountId, bool positive)
        {
            if (_context.Operations == null)
                throw new Exception("Database error");

            var account = await _context.Accounts.Where(o => o.Id == accountId).Include(o => o.Operations).Include(o => o.Categories).FirstOrDefaultAsync();
            if (account == null)
                throw new Exception("Account doesn't exist");

            //account operations grouped by category and counted
            var operations = account.Operations.Where(o => 
            {
                if (positive)
                    return o.Amount > 0;
                else 
                    return o.Amount < 0;

            }).GroupBy(p => p.CategoryId).
                Select(p => new { CategoryId = p.Key, Sum = p.Sum(x => x.Amount) });

            //get categories assigned to that account
            var categories = account.Categories;
            if (categories.First() == null)
                throw new Exception("Account doesn't have any categories");

            var result = new List<OperationByCategoryReportDTO>();

            foreach (Category category in categories)
            {
                var operationSum = operations.Where(o => o.CategoryId == category.Id).FirstOrDefault();//operations.First(o => o.CategoryId == category.Id);

                if (operationSum != null)
                    result.Add(new OperationByCategoryReportDTO()
                    {
                        Sum = operationSum.Sum,
                        Category = _mapper.Map<GetCategoryDTO>(category)
                    });
                //If sum wasn't found then it's 0
                else
                    result.Add(new OperationByCategoryReportDTO()
                    {
                        Category = _mapper.Map<GetCategoryDTO>(category)
                    });
            }

            return result;
        }
    }
}
