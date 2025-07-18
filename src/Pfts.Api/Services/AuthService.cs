namespace Pfts.Api.Services;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Pfts.Api.Models.Auth;
using Pfts.Api.Utils;
using Pfts.Domain.Exceptions;
using Pfts.Domain.Models;

public interface IAuthService
{
    public Task<IdentityResult> Register(RegisterRequest registration);
    public Task<TokenResponse> Login(LoginRequest login);
}

public class AuthService(
    UserManager<User> userManager,
    SignInManager<User> signInManager,
    IUserStore<User> userStore) : IAuthService
{
    private static readonly EmailAddressAttribute EmailAddressAttribute = new();

    public async Task<IdentityResult> Register(RegisterRequest registration)
    {
        if (!userManager.SupportsUserEmail)
        {
            throw new NotSupportedException($"{nameof(AuthService)} requires a user store with email support.");
        }

        var emailStore = (IUserEmailStore<User>)userStore;
        var email = registration.Email;
        var userName = registration.UserName;

        if (string.IsNullOrEmpty(email) || !EmailAddressAttribute.IsValid(email))
        {
            return IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(email));
        }

        var user = new User();
        await userStore.SetUserNameAsync(user, userName, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);

        var result = await userManager.CreateAsync(user, registration.Password);

        return result;
    }

    public async Task<TokenResponse> Login(LoginRequest login)
    {
        var user = await userManager.FindByNameAsync(login.UserName)
            ?? throw new AccessDeniedException("Username incorrect.");

        var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            throw new AccessDeniedException("Password incorrect.");
        }

        // Generate tokens

        var accessToken = await TokenUtils.GenerateAccessToken(user, "BE699EDC43FB42FC8D55303949BDAEEC", userManager);
        var refreshToken = TokenUtils.GenerateRefreshToken();

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}
