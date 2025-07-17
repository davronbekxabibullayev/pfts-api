namespace Pfts.Domain.Models;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string Username { get; set; } = default!;
    public List<Role> Roles { get; set; } = [];

    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
    public ICollection<AuditLog> AuditLogs { get; set; } = [];
}
