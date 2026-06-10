using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Application.Interfaces.Common;
using Src.Domain.Entities;
using Src.Infrastructure;
using Src.Infrastructure.Persistance;
using Src.Infrastructure.Repository;
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

    private static readonly Dictionary<Type, Type> ScopedServices = new Dictionary<Type, Type>
    {
        {typeof(IItemRepository),  typeof(ItemRepository)},
        {typeof(IFavouriteRepository),  typeof(FavouriteRepository)},
        {typeof(ICurrentUser),  typeof(HttpCurrentUser)}
    };

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
    /// Syncs the database roles with the roles defined in the <see cref="Roles"/> enum.
    /// Adds any missing roles and removes any stale roles that no longer exist in the enum.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationDbContext"/> used to access the database.</param>
    /// <param name="logger">The <see cref="ILogger"/> used for logging seeding operations.</param>
    public static async Task SyncRolesAsync(ApplicationDbContext context, ILogger logger)
    {
        var enumRoles = Enum.GetNames<Roles>().ToList();
        if (!context.AccountType.Any())
        {
            logger.LogInformation("No roles found in database. Seeding roles.");
            foreach (var role in enumRoles)
            {
                context.AccountType.Add(new AccountType { AccountTypeName = role });
                logger.LogInformation("Seeding role: {Role}", role);
            }
            await context.SaveChangesAsync();
            logger.LogInformation("Successfully seeded {Count} roles.", enumRoles.Count);
        }
        else
        {
            List<AccountType> rolesToRemove = new List<AccountType>();
            List<AccountType> rolesToAdd = new List<AccountType>();
            List<AccountType> accountTypeDbList = context.AccountType.ToList();
            logger.LogInformation("Syncing roles. Database has {DbCount} roles, enum has {EnumCount} roles.", accountTypeDbList.Count, enumRoles.Count);
            foreach (var AccountTypeName in accountTypeDbList)
            {
                var role = AccountTypeName.AccountTypeName.ToString();
                if (!enumRoles.Contains(role))
                {
                    rolesToRemove.Add(AccountTypeName);
                    logger.LogInformation("Role marked for removal: {Role}", role);
                }
            }
            foreach (var enumRole in enumRoles)
            {
                var tempRole = new AccountType { AccountTypeName = enumRole };
                if (!accountTypeDbList.Any(a => a.AccountTypeName == enumRole))
                {
                    rolesToAdd.Add(tempRole);
                    logger.LogInformation("Role marked for addition: {Role}", enumRole);
                }
            }

            //need to add user's role replacement to guest as well!

            foreach (var role in rolesToRemove)
            {
                context.AccountType.Remove(role);
            }
            foreach (var role in rolesToAdd)
            {
                context.AccountType.Add(role);
            }
            await context.SaveChangesAsync();
            logger.LogInformation("Role sync complete. Added: {Added}, Removed: {Removed}.", rolesToAdd.Count, rolesToRemove.Count);
        }
    }

    public static void HandleRepositoryScopedServices(WebApplicationBuilder builder)
    {
        foreach (var service in ScopedServices)
        {
            builder.Services.AddScoped(service.Key, service.Value);
        }

    }
}