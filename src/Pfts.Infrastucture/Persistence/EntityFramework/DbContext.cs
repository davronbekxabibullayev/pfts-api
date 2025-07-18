namespace Uds.Identity.Infrastructure.Persistence.EntityFramework;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Persistance;
using Domain.Enums;
using Domain.Models;

public class DbContext : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, IDbContext
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public DbContext(DbContextOptions<DbContext> options) : base(options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }

    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
    public override DbSet<Role> Roles { get; set; }
    public override DbSet<User> Users { get; set; }
    public DbSet<UserTicket> UserTickets { get; set; }
    public DbSet<CaptchaRecord> CaptchaRecords { get; set; }
    public override DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserOrganization> UserOrganizations { get; set; }

    public IExecutionStrategy CreateExecutionStrategy()
    {
        return Database.CreateExecutionStrategy();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<User>().HasMany(a => a.Roles)
            .WithOne(a => a.User)
            .HasForeignKey(a => a.UserId);

        builder.Entity<UserRole>(b => b.ToTable(TableConsts.AspNetUserRoles));
        builder.Entity<UserClaim>(b => b.ToTable(TableConsts.AspNetUserClaims));
        builder.Entity<UserLogin>(b => b.ToTable(TableConsts.AspNetUserLogins));
        builder.Entity<UserToken>(b => b.ToTable(TableConsts.AspNetUserTokens));
        builder.Entity<RoleClaim>(b => b.ToTable(TableConsts.AspNetRoleClaims));

        // UserRole konfiguratsiyasi
        builder.Entity<UserRole>(entity =>
        {
            entity.ToTable(TableConsts.AspNetUserRoles);

            entity.HasKey(ur => ur.Id);

            entity.Property(ur => ur.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasIndex(ur => new { ur.UserId, ur.RoleId })
                .HasFilter(@"""OrganizationId"" IS NULL")
                .IsUnique();

            entity.HasIndex(ur => new { ur.UserId, ur.RoleId, ur.OrganizationId })
                .IsUnique();

        });

        builder.Entity<Role>(entity =>
        {
            entity.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(r => r.RoleClaims)
                .WithOne(rc => rc.Role)
                .HasForeignKey(rc => rc.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<UserOrganization>(entity =>
        {
            entity.HasIndex(a => new { a.UserId, a.OrganizationId }).IsUnique();
            entity.HasIndex(a => a.OrganizationId);
        });

        builder.Entity<UserTicket>(entity =>
        {
            entity.HasKey(a => a.Id);
        });

        builder.Entity<CaptchaRecord>(entity =>
        {
            entity.HasKey(a => a.Id);
        });
    }
}
