using System.ComponentModel.DataAnnotations;

namespace BudgetOrganizer.Models
{
    public class AccountLoginData
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
