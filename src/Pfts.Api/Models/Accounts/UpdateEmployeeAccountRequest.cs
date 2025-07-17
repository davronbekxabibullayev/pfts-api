namespace Pfts.Api.Models.Accounts;

public class UpdateEmployeeAccountRequest
{
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Avatar { get; set; }
    public List<string>? Roles { get; init; }
}
