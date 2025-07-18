namespace Pfts.Domain.Models;

using Pfts.Domain.Common;
using Pfts.Domain.Enum;

public class Transaction : Entity, IDeletable
{
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public Guid CategoryId { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Note { get; set; }
    public byte[] RowVersion { get; set; } = default!;

    public virtual Category? Category { get; set; }
    public virtual User? User { get; set; }
    public bool IsDeleted { get; set; }
}
