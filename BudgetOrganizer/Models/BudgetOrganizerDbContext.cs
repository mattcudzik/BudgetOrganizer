using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.OperationModel;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.RoleModel;

namespace BudgetOrganizer.Models
{
    public class BudgetOrganizerDbContext : DbContext
    {
        public BudgetOrganizerDbContext(DbContextOptions options) : base(options)
        {
            
        }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationImage> OperationImages { get; set; }
        public DbSet<Account> Accounts { get; set; }
    }
}
