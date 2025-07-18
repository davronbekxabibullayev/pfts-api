namespace Pfts.Application.Common;

using System;

public interface ICurrentUser
{
    public Guid UserId { get; }
    public string[] Roles { get; }
    public bool IsAdmin { get; }
}
