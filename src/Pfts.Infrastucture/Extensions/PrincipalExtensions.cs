namespace Pfts.Infrastucture.Extensions;

using System;
using System.Security.Claims;
using System.Security.Principal;
using IdentityModel;
using Pfts.Infrastucture.Common.Constants;

public static class PrincipalExtensions
{
    /// <summary>
    /// Gets the subject identifier.
    /// </summary>
    public static string GetSubjectId(this IPrincipal principal)
    {
        return principal.Identity?.GetSubjectId() ?? throw new InvalidOperationException("sub claim is missing");
    }

    /// <summary>
    /// Gets the subject identifier.
    /// </summary>
    public static string GetSubjectId(this IIdentity identity)
    {
        var id = identity as ClaimsIdentity;
        var claim = id?.FindFirst(ClaimTypes.NameIdentifier);

        var result = claim?.Value ?? throw new InvalidOperationException("sub claim is missing");

        return result;
    }

    public static string GetSubjectEmployeeId(this IIdentity identity)
    {
        var userId = identity as ClaimsIdentity;

        var claim = userId?.FindFirst(ApplicationClaimTypes.UserId);

        var result = claim?.Value ?? throw new InvalidOperationException("uid claim is missing");

        return result;
    }

    public static string[]? GetRoles(this IIdentity identity)
    {
        var claims = identity as ClaimsPrincipal;

        var roles = claims?.Claims.Where(a => a.Type is ClaimTypes.Role or "role").Select(s => s.Value).ToArray();

        return roles;
    }

    public static string GetRole(this IIdentity identity)
    {
        var claimsPrincipal = identity as ClaimsPrincipal;

        var role = claimsPrincipal?.FindFirst(ClaimTypes.Role)?.Value ?? claimsPrincipal?.FindFirst("role")?.Value ?? string.Empty;

        return role;
    }

    public static string GetUserName(this IIdentity identity)
    {
        var claimsPrincipal = identity as ClaimsPrincipal;

        var userName = claimsPrincipal?.FindFirst(JwtClaimTypes.Name)?.Value ?? string.Empty;

        return userName;
    }

    public static bool HasRole(this IIdentity identity, string roleName)
    {
        var claimsPrincipal = identity as ClaimsPrincipal;

        return claimsPrincipal?.FindFirst(ClaimTypes.Role)?.Value == roleName || claimsPrincipal?.FindFirst("role")?.Value == roleName;
    }

    public static bool HasPermission(this IIdentity identity, string permissionName)
    {
        var claimsPrincipal = identity as ClaimsPrincipal;

        return claimsPrincipal?.FindFirst(ApplicationClaimTypes.Permission)?.Value == permissionName || claimsPrincipal?.FindFirst("permission")?.Value == permissionName;
    }
}
