namespace Pfts.Domain.Models;

using Pfts.Domain.Common;

public class AuditLog : Entity
{
    public Guid UserId { get; set; }
    public string Action { get; set; } = default!;
    public string EntityName { get; set; } = default!;
    public Guid EntityId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }

    public virtual User? User { get; set; }
}
