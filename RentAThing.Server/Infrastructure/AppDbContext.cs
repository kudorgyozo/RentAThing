using Microsoft.EntityFrameworkCore;
using RentAThing.Server.Models;

namespace RentAThing.Server.Infrastructure; 
public class AppDbContext : DbContext {

    public DbSet<RentalItem> RentalItems { get; set; } = null!;

    public DbSet<Car> Cars { get; set; } = null!;

    public DbSet<Bike> Bikes { get; set; } = null!;

    public DbSet<ElectricScooter> ElectricScooters { get; set; } = null!;

    public DbSet<User> Users { get; set; }

    public DbSet<RentHistory> RentHistory { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    modelBuilder.Entity<Category>()
        //.HasMany(c => c.Products)
        //.WithOne(p => p.Category)
        //.HasForeignKey(p => p.CategoryId);

        //modelBuilder.Entity<RentalItem>()
        //    .HasOne(ri => ri.Renter)
        //    .WithMany(u => u.RentalItems)
        //    .HasForeignKey(ri => ri.RenterId);

        modelBuilder.Entity<RentalItem>()
            .HasDiscriminator<string>("Type")
            .HasValue<Car>("Car")
            .HasValue<Bike>("Bike")
            .HasValue<ElectricScooter>("ElectricScooter");

        modelBuilder.Entity<User>()
            .HasMany(u => u.RentalItems)
            .WithOne(ri => ri.Renter)
            .HasForeignKey(ri => ri.RenterId);
    }

    public override void Dispose() {
        base.Dispose();
    }

}
