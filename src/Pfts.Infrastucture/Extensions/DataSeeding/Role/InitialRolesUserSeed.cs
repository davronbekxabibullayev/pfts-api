namespace Pfts.Infrastucture.Extensions.DataSeeding.Role;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Pfts.Domain.Models;
using Pfts.Infrastucture.Common.Constants;
using Pfts.Infrastucture.Persistence.EntityFramework;
using static Pfts.Infrastucture.Common.Constants.Permissions;

public class InitialRolesUserSeed
{
    public async Task SeedAsync(AppDbContext context, IServiceProvider services, int retry = 0)
    {
        var executionStrategy = context.Database.CreateExecutionStrategy();
        await executionStrategy.ExecuteAsync(
            () => ProccessSeedAsync(context, services, retry));
    }

    private async Task ProccessSeedAsync(AppDbContext context, IServiceProvider services, int retry = 0)
    {
        var env = services.GetRequiredService<IWebHostEnvironment>();
        var logger = services.GetRequiredService<ILogger<InitialRolesUserSeed>>();
        using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            if (!context.Roles.Any())
            {
                var roles = GetDefaultRoles();
                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();

                foreach (var role in roles)
                {
                    context.RoleClaims.AddRange(GetDefaultRoleClaims(role));
                }
                await context.SaveChangesAsync();
            }

            if (!context.Users.Any())
            {
                var users = GetDefaultUsers();
                context.Users.AddRange(users);
                context.UserRoles.AddRange(GetDefaultUserRoles(users));
                await context.SaveChangesAsync();
            }

            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(AppDbContext));

            if (retry >= 3)
                throw;

            await SeedAsync(context, services, ++retry);
        }
    }

    private static IEnumerable<Role> GetDefaultRoles()
    {
        var roles = Roles.List().Select(r => new Role
        {
            Id = r.Id,
            Name = r.Name,
            NormalizedName = r.NormilizedName,
            ConcurrencyStamp = Guid.NewGuid().ToString("D")
        });

        return roles;
    }

    private static IEnumerable<IdentityRoleClaim<Guid>> GetDefaultRoleClaims(Role role)
    {
        if (role.Name == Roles.Admin.Name)
        {
            yield return new IdentityRoleClaim<Guid> { ClaimType = ApplicationClaimTypes.Permission, ClaimValue = Admin.Logs, RoleId = role.Id };
        }

        if (role.Name == Roles.Driver.Name)
        {
            yield return new IdentityRoleClaim<Guid> { ClaimType = ApplicationClaimTypes.Permission, ClaimValue = Admin.Logs, RoleId = role.Id };
        }
    }

    private static User[] GetDefaultUsers()
    {
        var passwordHasher = new PasswordHasher<User>();

        var adminUser = new User
        {
            Id = Guid.Parse("60CE452C-6778-47EC-BA07-C745E7BA6C04"),
            EmailConfirmed = true,
            PhoneNumberConfirmed = false,
            UserName = "admin",
            Email = "bepro@info.uz",
            NormalizedUserName = "ADMIN",
            NormalizedEmail = "BEPRO@INFO.UZ",
            SecurityStamp = Guid.NewGuid().ToString("D"),
            ConcurrencyStamp = "fcd781ef-affc-4020-ab02-f636b3db4c23",
        };

        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "123456Test$");

        return [adminUser];
    }

    private static IdentityUserRole<Guid>[] GetDefaultUserRoles(User[] users)
    {
        var adminUserRoles = new IdentityUserRole<Guid>[]
        {
            new ()
            {
                RoleId = Roles.Admin.Id,
                UserId = users[0].Id
            }
        };

        return adminUserRoles;
    }
}
