namespace Pfts.Infrastructure.Persistance.EntityFramework.EntityConfigurations;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Pfts.Domain.Models;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public virtual void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(a => a.Username).HasMaxLength(256)
            .IsRequired(false);
    }
}
