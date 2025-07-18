namespace Pfts.Infrastucture.Common.Constants;

public static class RoleNames
{
    public const string Admin = "Администратор Глобальный";
    public const string Driver = "Водитель";
}

public static class Roles
{
    public static readonly RoleInfo Admin = new(Guid.Parse("606AC764-3EF8-49B0-9430-5BA92F4142EA"), nameof(RoleNames.Admin));
    public static readonly RoleInfo Driver = new(Guid.Parse("D1261E68-DCFF-407D-A657-3EC7B4A1153C"), nameof(RoleNames.Driver));

    public static List<RoleInfo> List()
    {
        var list = typeof(Roles)
            .GetFields()
            .Where(x => x.IsStatic && x.IsPublic && x.FieldType == typeof(RoleInfo))
            .Select(x => x.GetValue(null))
            .OfType<RoleInfo>()
            .ToList();

        return list;
    }
}

public readonly struct RoleInfo(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public string NormilizedName { get; } = name.ToUpper(System.Globalization.CultureInfo.CurrentCulture);
}
