using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.CategoryModel
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [StringLength(7, MinimumLength = 7)]
        public string Color { get; set; }

        //delete?
        public ICollection<Account> Accounts { get; } = new List<Account>();
    }
}
