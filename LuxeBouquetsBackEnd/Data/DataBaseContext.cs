using LuxeBouquetsBackEnd.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuxeBouquetsBackEnd.Controllers
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}