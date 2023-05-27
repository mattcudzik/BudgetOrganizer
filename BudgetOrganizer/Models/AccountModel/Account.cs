using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.ProfileModel;
using Microsoft.AspNetCore.Identity;

namespace BudgetOrganizer.Models.AccountModel
{
    public class Account : IdentityUser
    {
        //public Guid Id { get; set; }
        //public string Login { get; set; }
        public string TESTASAS { get; set; }
        //[EmailAddress]
       // public string Email { get; set; }
        public ICollection<Profile> Profiles { get; } = new List<Profile>();
    }
}
