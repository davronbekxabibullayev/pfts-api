namespace Pfts.Api.Utils;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Pfts.Domain.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public static class TokenUtils
{
    public static async Task<string> GenerateAccessToken(User user, string secret, UserManager<User> userManager)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = "ravm",
            Audience = "ravm",
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("sub", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(24), // Token expiration time
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var roles = await userManager.GetRolesAsync(user);

        var s = roles.Select(role => new Claim(ClaimsIdentity.DefaultRoleClaimType, role));

        tokenDescriptor.Subject.AddClaims(s);

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static string GenerateAccessTokenFromRefreshToken(string refreshToken, string secret)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secret);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
