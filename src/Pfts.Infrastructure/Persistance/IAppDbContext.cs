namespace Pfts.Infrastructure.Persistance;

using Microsoft.EntityFrameworkCore;
using Pfts.Domain.Models;

public interface IAppDbContext : IDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

}
