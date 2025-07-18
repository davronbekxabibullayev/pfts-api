namespace Pfts.Api.Controllers;

using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pfts.Api.Services;
using Pfts.Api.Models.Auth;

[Route("auth")]
[ApiController]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest login)
    {
        var result = await authService.Login(login);

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<Results<Ok, ValidationProblem>> Register(RegisterRequest registration)
    {
        var result = await authService.Register(registration);

        if (!result.Succeeded)
        {
            return CreateValidationProblem(result);
        }

        return TypedResults.Ok();
    }

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        Debug.Assert(!result.Succeeded);
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }
}
