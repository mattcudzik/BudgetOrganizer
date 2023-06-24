using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.GroupModel
{
    public class Group
    {
        public Guid Id { get; set; }
        public List<Account> Accounts { get; } = new List<Account>();
    }
}
