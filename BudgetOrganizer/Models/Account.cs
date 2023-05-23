namespace BudgetOrganizer.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Login { get; set; }  
        public string Password { get; set; }
        public string Email { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
