namespace Pfts.Application.Common;

using Microsoft.EntityFrameworkCore;
using Pfts.Domain.Models;

public interface IAppDbContext : IDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Category> Categories { get; set; }
    DbSet<Transaction> Transactions { get; set; }
    DbSet<AuditLog> AuditLogs { get; set; }

}
