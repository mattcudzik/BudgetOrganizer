using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.AccountModel;

namespace BudgetOrganizer.Models.ProfileModel
{
	public class GetProfileDTO
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Surname { get; set; }
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }


        [ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public Role Role { get; set; }
    }
}
