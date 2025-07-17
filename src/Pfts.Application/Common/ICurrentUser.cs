namespace Pfts.Application.Common;

using System;

public interface ICurrentUser
{
    public Guid UserId { get; }
    public Guid EmployeeId { get; }
    public Guid OrganizationId { get; }
    public string[] Roles { get; }
    public bool IsAdmin { get; }
}
