using System.ComponentModel.DataAnnotations;
using BudgetOrganizer.Models.ProfileModel;
using Microsoft.AspNetCore.Identity;

namespace BudgetOrganizer.Models
{
    public class AccountIdentity : IdentityUser
    {
        public ICollection<Profile> Profiles { get; } = new List<Profile>();
    }
}
