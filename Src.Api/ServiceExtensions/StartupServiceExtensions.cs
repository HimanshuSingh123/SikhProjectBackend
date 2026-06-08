using Microsoft.Extensions.Logging;
using Src.Domain.Entities;
using Src.Infrastructure.Persistance;
namespace Src.Api.ServiceExtensions;
/// <summary>
/// Provides startup service extension methods for configuring authorization policies and seeding initial data.
/// </summary>
public static class StartupServiceExtensions
{
    /// <summary>
    /// Defines all available roles in the system.
    /// This enum is the source of truth for roles and is used to seed the database on startup.
    /// </summary>
    private enum Roles
    {
        Admin,
        Host,
        Instructor,
        Publisher,
        SysAdmin,
        User,
        Vendor,
        Guest
    }
    /// <summary>
    /// Registers authorization policies and configures role-based access control.
    /// Also responsible for seeding roles into the database on startup.
    /// </summary>
    /// <param name="builder">The <see cref="WebApplicationBuilder"/> used to configure services.</param>
    /// <param name="context">The <see cref="ApplicationDbContext"/> used to seed roles into the database.</param>
    /// <param name="logger">The <see cref="ILogger"/> used for logging startup operations.</param>
    public static void HandleRoles(WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireRole(Roles.Admin.ToString()));
            options.AddPolicy("HighPriviledge", policy => policy.RequireRole(Roles.Admin.ToString(), Roles.SysAdmin.ToString()));
            options.AddPolicy("ContentCreator", policy => policy.RequireRole(Roles.Instructor.ToString(), Roles.Publisher.ToString(), Roles.Vendor.ToString()));
            options.AddPolicy("Guest", policy => policy.RequireRole(Enum.GetNames<Roles>().ToList()));
        });
    }
    /// <summary>
    /// Seeds the database with all roles defined in the <see cref="Roles"/> enum if they do not already exist.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationDbContext"/> used to access the database.</param>
    /// <param name="logger">The <see cref="ILogger"/> used for logging seeding operations.</param>
    public static async Task SeedRolesAsync(ApplicationDbContext context, ILogger logger)
    {
        if (!context.AccountType.Any())
        {
            logger.LogInformation("No roles found in database. Seeding roles...");
            var roles = Enum.GetNames<Roles>().ToList();
            foreach (var role in roles)
            {
                context.AccountType.Add(new AccountType { AccountTypeName = role });
                logger.LogInformation("Seeding role: {Role}", role);
            }
            await context.SaveChangesAsync();
            logger.LogInformation("Successfully seeded {Count} roles.", roles.Count);
        }
        else
        {
            //TODO: Remove from DB if that data is not present
            logger.LogInformation("Roles already exist in database. Skipping seeding.");
        }
    }
}