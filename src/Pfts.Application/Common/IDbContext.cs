namespace Pfts.Application.Common;

using Microsoft.EntityFrameworkCore.Infrastructure;

public interface IDbContext
{
    public int SaveChanges();
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public DatabaseFacade Database { get; }
}
