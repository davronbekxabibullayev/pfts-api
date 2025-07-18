namespace Pfts.Domain.Models;

using Pfts.Domain.Common;

public class Category : Entity, IDeletable
{
    public string Name { get; set; } = default!;
    public string Color { get; set; } = default!;
    public Guid UserId { get; set; }

    public virtual User? User { get; set; }
    public ICollection<Transaction> Transactions { get; set; } = [];
    public bool IsDeleted { get; set; }
}
