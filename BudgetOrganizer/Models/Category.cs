using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.ProfileModel;

namespace BudgetOrganizer.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public ICollection<Account> Accounts { get; } = new List<Account>();
    }
}
