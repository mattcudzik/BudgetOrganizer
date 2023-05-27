using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.ProfileModel;
using BudgetOrganizer.Models.OperationModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BudgetOrganizer.Models
{
    public class BudgetOrganizerDbContext : IdentityDbContext<Account>
    {
        public BudgetOrganizerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationImage> OperationImages { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
