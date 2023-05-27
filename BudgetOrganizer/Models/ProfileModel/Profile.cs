using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.OperationModel;

namespace BudgetOrganizer.Models.ProfileModel
{
    public class Profile
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string? Surname { get; set; }
        [Range(0, 9999)]
        public int? PinNumber { get; set; }
        public decimal Budget { get; set; }
        public decimal? SpendingLimit { get; set; }


        [ForeignKey("Role")]
		public Guid RoleId { get; set; }
		public Role Role { get; set; }

		[ForeignKey("Account")]
		public Guid AccountId { get; set; }
		public Account Account { get; set; }

		public ICollection<Operation> Operations { get; } = new List<Operation>();
		public ICollection<Category> Categories { get; } = new List<Category>();

	}
}
