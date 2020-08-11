using System;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;

namespace PizzaStore.Storing
{
    public class PizzaStoreDbContext : DbContext
    {
        public DbSet<PizzaModel> Pizzas { get; set; }
        public DbSet<MenuPizzaModel> MenuPizzas { get; set; }
        public DbSet<CrustModel> Crusts { get; set; }
        public DbSet<SizeModel> Sizes { get; set; }
        public DbSet<ToppingModel> Toppings { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<StoreModel> Stores { get; set; }
        public DbSet<PizzaToppingModel> PizzaToppings { get; set; }
        public DbSet<MenuPizzaToppingModel> MenuPizzaToppings { get; set; }

        public PizzaStoreDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CrustModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_CrustId");
                entity.Property(e => e.Price).HasColumnType("money");
            });

            builder.Entity<SizeModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_SizeId");
                entity.Property(e => e.Price).HasColumnType("money");
            });

            builder.Entity<ToppingModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ToppingId");
                entity.Property(e => e.Price).HasColumnType("money");
            });

            builder.Entity<UserModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_UserId");
            });

            builder.Entity<StoreModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_StoreId");
            });

            builder.Entity<OrderModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OrderId");
                entity.Property(e => e.Price)
                    .HasColumnType("money");

                entity.Property(e => e.PurchaseDate)
                    .HasColumnType("datetime2(0)")
                    .HasDefaultValue(DateTime.UtcNow);

                entity.Property(e => e.Submitted)
                    .HasDefaultValue(false);

                entity.HasOne(d => d.StoreSubmitted)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StoreSubmittedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreSubmittedId");

                entity.HasOne(d => d.UserSubmitted)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserSubmittedId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserSubmittedId");
            });

            builder.Entity<PizzaModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_PizzaId");
                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK_PizzaCrustId");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK_PizzaSizeId");

                entity.Ignore(e => e.Toppings);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderId");
            });

            builder.Entity<MenuPizzaModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_MenuPizzaId");
                entity.Property(e => e.CrustId).IsRequired(false);

                entity.HasOne(d => d.Crust)
                    .WithMany(p => p.MenuPizzas)
                    .HasForeignKey(d => d.CrustId)
                    .HasConstraintName("FK_MenuCrustId");

                entity.Ignore(e => e.Toppings);
                entity.Ignore(e => e.Price);
            });

            builder.Entity<PizzaToppingModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_PizzaToppingId");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaToppings)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PizzaId");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.PizzaToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ToppingId");
            });

            builder.Entity<MenuPizzaToppingModel>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_MenuPizzaToppingId");

                entity.HasOne(d => d.MenuPizza)
                    .WithMany(p => p.MenuPizzaToppings)
                    .HasForeignKey(d => d.MenuPizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuPizzaId");

                entity.HasOne(d => d.Topping)
                    .WithMany(p => p.MenuPizzaToppings)
                    .HasForeignKey(d => d.ToppingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuToppingId");
            });
        }
    }
}