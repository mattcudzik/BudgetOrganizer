using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.ProfileModel
{
    public class Profile
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [ForeignKey("Role")]
        public Guid RoleId { get; set; }
        [Required]
        public Role Role { get; set; }
        public int? PinNumber { get; set; }
        [Required]
        public decimal Budget { get; set; }
        [ForeignKey("Account")]
        public Guid AccountId { get; set; }
        [Required]
        public Account Account { get; set; }
        public decimal? SpendingLimit { get; set; }
        public ICollection<Operation> Operations { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
