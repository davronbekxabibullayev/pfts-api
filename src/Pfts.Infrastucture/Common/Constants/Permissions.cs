namespace Pfts.Infrastructure.Common.Constants;

public readonly struct PermissionInfo(string key, string group, string displayName, string displayNameRu, string displayNameKa, string displayNameEn)
{
    public string Key { get; } = key;
    public string DisplayName { get; } = displayName;
    public string DisplayNameRu { get; } = displayNameRu;
    public string DisplayNameKa { get; } = displayNameKa;
    public string DisplayNameEn { get; } = displayNameEn;
    public string Group { get; } = group;
}
public readonly struct Permissions
{
    public struct Admin
    {
        public const string Logs = "1001";
        public const string UserSystemSettings = "1002";
    }

    public static readonly List<PermissionInfo> List =
    [
        new PermissionInfo(Admin.Logs, "Администрирование", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала", "Доступ к просмотру Лог-журнала"),
        new PermissionInfo(Admin.UserSystemSettings, "Администрирование", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы", "Доступ к пользовательским настройкам системы"),
    ];
}
