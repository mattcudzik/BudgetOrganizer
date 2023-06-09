using Microsoft.EntityFrameworkCore;
using BudgetOrganizer.Models.AccountModel;
using BudgetOrganizer.Models.OperationModel;
using BudgetOrganizer.Models.CategoryModel;
using BudgetOrganizer.Models.RoleModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BudgetOrganizer.Models.GroupModel;

namespace BudgetOrganizer.Models
{
    public class BudgetOrganizerDbContext : IdentityDbContext<Account, BudgetOrganizerRole, Guid>
    {
        public readonly String[] defaultCategories = { "Zakupy", "Rachunki", "Transport", "Rozrywka i wypoczynek", "Zdrowie", "Edukacja", "Dzieci", "Inne", "Kieszonkowe", "Emerytura", "Sprzedaż", "Wynagrodzenie" };
        public BudgetOrganizerDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Role>().HasData(
                new Role 
                { 
                    Id = Guid.NewGuid(),
                    Name = "child"
                }, 
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "adult"
                });

            builder.Entity<Category>().HasData(AddDefultCategories());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<OperationImage> OperationImages { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Group> Groups { get; set; }

        private Category[] AddDefultCategories()
        {
            var categories = new Category[defaultCategories.Length];
            int i = 0;
            foreach (var categoryName in defaultCategories)
            {
                var color = ColorFromHSV(i * (double)360 / defaultCategories.Length, 0.9, 0.9);

                var category = new Category()
                {
                    Name = categoryName,
                    Id = Guid.NewGuid(),
                    Color = color
                };

                categories[i] = category;
                i++;
            }

            return categories;
        }

        //hue 0-360 
        private string ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return "#" + v.ToString("X2") + t.ToString("X2") + p.ToString("X2");
            else if (hi == 1)
                return "#" + q.ToString("X2") + v.ToString("X2") + p.ToString("X2");
            else if (hi == 2)
                return "#" + p.ToString("X2") + v.ToString("X2") + t.ToString("X2");
            else if (hi == 3)
                return "#" + p.ToString("X2") + q.ToString("X2") + v.ToString("X2");
            else if (hi == 4)
                return "#" + t.ToString("X2") + p.ToString("X2") + v.ToString("X2");
            else
                return "#" + v.ToString("X2") + p.ToString("X2") + q.ToString("X2");
        }
    }
}
