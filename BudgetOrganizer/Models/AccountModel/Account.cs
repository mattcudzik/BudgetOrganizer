using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.ProfileModel;

namespace BudgetOrganizer.Models.AccountModel
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public ICollection<Profile> Users { get; } = new List<Profile>();
    }
}
