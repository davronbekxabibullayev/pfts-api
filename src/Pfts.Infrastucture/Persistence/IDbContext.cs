namespace Uds.Identity.Infrastructure.Persistance;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

public interface IDbContext : IDataProtectionKeyContext
{
    new DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    DbSet<UserRole> UserRoles { get; set; }
    new DbSet<Role> Roles { get; set; }
    DbSet<RoleClaim> RoleClaims { get; set; }
    DbSet<UserOrganization> UserOrganizations { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<Role> Roles { get; set; }
    DbSet<UserTicket> UserTickets { get; set; }

    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    IExecutionStrategy CreateExecutionStrategy();
    DatabaseFacade Database { get; }
}
