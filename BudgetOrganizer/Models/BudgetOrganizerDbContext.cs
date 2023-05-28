using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.ProfileModel;
using BudgetOrganizer.Models.OperationModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BudgetOrganizer.Models
{
    public class BudgetOrganizerDbContext : IdentityDbContext<AccountIdentity>
    {
        public BudgetOrganizerDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Role> ProfileRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationImage> OperationImages { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
