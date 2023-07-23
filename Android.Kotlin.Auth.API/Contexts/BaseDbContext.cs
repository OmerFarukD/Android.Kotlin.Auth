using Android.Kotlin.Auth.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Android.Kotlin.Auth.API.Contexts;

public class BaseDbContext : DbContext
{

    public BaseDbContext(DbContextOptions<BaseDbContext> context) : base(context)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(x => x.Id);
        modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired();
        modelBuilder.Entity<Product>().Property(x => x.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<Product>().Property(x => x.Stock).IsRequired();
        modelBuilder.Entity<Product>().Property(x => x.Color).IsRequired();
        
        
        modelBuilder.Entity<Category>().HasKey(x => x.Id);
        modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired();
        modelBuilder
            .Entity<Category>()
            .HasMany(x => x.Products)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        base.OnModelCreating(modelBuilder);
        
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}