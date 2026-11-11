namespace ArchPZ.Demo.Infrastructure.Persistence;

using ArchPZ.Demo.Domain.OrderManagement.Entities;
using ArchPZ.Demo.Domain.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<User> Users => Set<User>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.OwnsOne(o => o.ShippingAddress);
            builder.HasMany(o => o.Items).WithOne();
            builder.Ignore(o => o.DomainEvents);
        });

        modelBuilder.Entity<OrderItem>(builder =>
        {
            builder.HasKey(i => i.Id);
            builder.OwnsOne(i => i.UnitPrice);
        });

        modelBuilder.Entity<User>(builder =>
        {
            builder.HasKey(u => u.Id);
            builder.OwnsOne(u => u.Name);
            builder.OwnsOne(u => u.Email);
            builder.Ignore(u => u.DomainEvents);
        });
    }
}
