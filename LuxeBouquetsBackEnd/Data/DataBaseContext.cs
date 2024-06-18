using LuxeBouquetsBackEnd.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuxeBouquetsBackEnd.Controllers
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity is Category category)
                {
                    var now = DateTime.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            category.CreatedAt = now;
                            category.UpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            category.UpdatedAt = now;
                            break;
                    }
                }
                else if (entry.Entity is Product product)
                {
                    var now = DateTime.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            product.CreatedAt = now;
                            product.UpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            product.UpdatedAt = now;
                            break;
                    }
                }
                else if (entry.Entity is SubscriptionPlan subscriptionPlan)
                {
                    var now = DateTime.UtcNow;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            subscriptionPlan.CreatedAt = now;
                            subscriptionPlan.UpdatedAt = now;
                            break;
                        case EntityState.Modified:
                            subscriptionPlan.UpdatedAt = now;
                            break;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}