namespace Pfts.Application.Common;

using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IDbContext
{
    int SaveChanges();
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    DatabaseFacade Database { get; }
}
