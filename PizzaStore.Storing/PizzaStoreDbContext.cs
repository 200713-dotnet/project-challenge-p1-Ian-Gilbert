using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing
{
    public class PizzaStoreDbContext : DbContext
    {
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<CrustModel> Crusts { get; set; }
        public DbSet<SizeModel> Sizes { get; set; }
        public DbSet<ToppingModel> Toppings { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StoreModel> Stores { get; set; }

        public PizzaStoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PizzaModel>().HasKey(e => e.Id);
            builder.Entity<CrustModel>().HasKey(e => e.Id);
            builder.Entity<SizeModel>().HasKey(e => e.Id);
            builder.Entity<ToppingModel>().HasKey(e => e.Id);
            builder.Entity<OrderModel>().HasKey(e => e.Id);
            builder.Entity<UserModel>().HasKey(e => e.Id);
            builder.Entity<StoreModel>().HasKey(e => e.Id);
        }
    }
}