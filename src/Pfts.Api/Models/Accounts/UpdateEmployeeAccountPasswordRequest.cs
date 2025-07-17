namespace Pfts.Api.Models.Accounts;

public class UpdateEmployeeAccountPasswordRequest
{
    public required Guid UserId { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }
}
