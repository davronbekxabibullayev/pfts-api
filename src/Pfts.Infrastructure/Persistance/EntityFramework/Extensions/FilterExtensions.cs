namespace Pfts.Infrastructure.Persistance.EntityFramework.Extensions;

using Microsoft.EntityFrameworkCore;
using Pfts.Domain.Common;
using System.Reflection;

public static class FilterExtensions
{
    public static ModelBuilder SetSoftDeleteFilter(this ModelBuilder modelBuilder, Type entityType)
    {
        SetSoftDeleteFilterMethod.MakeGenericMethod(entityType)
            .Invoke(null, [modelBuilder]);

        return modelBuilder;
    }

    private static readonly MethodInfo SetSoftDeleteFilterMethod = typeof(FilterExtensions)
               .GetMethods(BindingFlags.Public | BindingFlags.Static)
               .Single(t => t.IsGenericMethod && t.Name == nameof(SetSoftDeleteFilter));

    public static ModelBuilder SetSoftDeleteFilter<TEntity>(this ModelBuilder modelBuilder)
        where TEntity : class, IDeletable
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(x => !x.IsDeleted);

        return modelBuilder;
    }

    public static ModelBuilder SetSoftDeleteFilter(this ModelBuilder modelBuilder)
    {
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IDeletable).IsAssignableFrom(type.ClrType))
                modelBuilder.SetSoftDeleteFilter(type.ClrType);
        }

        return modelBuilder;
    }
    public static ModelBuilder ConfigureLocalizableEntities(this ModelBuilder modelBuilder)
    {
        foreach (var type in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IEntity).IsAssignableFrom(type.ClrType))
                modelBuilder.ConfigureEnitity(type.ClrType);
        }

        return modelBuilder;
    }

    public static ModelBuilder ConfigureEnitity(this ModelBuilder modelBuilder, Type entityType)
    {
        ConfigureLocalizableEnitityMethod.MakeGenericMethod(entityType)
            .Invoke(null, [modelBuilder]);

        return modelBuilder;
    }

    public static readonly MethodInfo ConfigureLocalizableEnitityMethod = typeof(FilterExtensions)
              .GetMethods(BindingFlags.Public | BindingFlags.Static)
              .Single(t => t.IsGenericMethod && t.Name == nameof(ConfigureEnitity));

}

