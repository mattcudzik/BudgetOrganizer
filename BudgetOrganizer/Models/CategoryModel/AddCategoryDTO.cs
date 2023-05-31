using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.CategoryModel
{
    public class AddCategoryDTO
    {
        public string Name { get; set; }
        [StringLength(6,MinimumLength = 6)]
        public string Color { get; set; }
    }
}
