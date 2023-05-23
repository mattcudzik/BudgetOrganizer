using Microsoft.EntityFrameworkCore;

namespace BudgetOrganizer.Models
{
    public class RoleDbContext : DbContext
    {
        public RoleDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Role> Roles { get; set; } = null!;
    }
}
