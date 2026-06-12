using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;
using Src.Domain.Entities;

namespace Src.Infrastructure.Persistance;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<AccountPermissions> AccountPermissions { get; set; }
    public DbSet<AccountType> AccountType { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Favourites> Favourites { get; set; }
    public DbSet<IndividualPermissions> IndividualPermissions { get; set; }
    public DbSet<Merch> Merch { get; set; }
    public DbSet<Newsfeed> Newsfeed { get; set; }
    public DbSet<Prayers> Prayers { get; set; }
    public DbSet<PurchaseHistory> PurchaseHistory { get; set; }
    public DbSet<SikhEvent> SikhEvent { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Review> Review { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    // To add a migration run from solution root:
    // dotnet ef migrations add <MigrationName> --project Src.Infrastructure --startup-project Src.Api
    // --project → where DbContext lives
    // --startup-project → where program.cs and connection string lives
}
