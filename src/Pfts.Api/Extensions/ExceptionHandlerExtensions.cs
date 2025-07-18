namespace Pfts.Api.Extensions;

using System.Globalization;
using System.Linq;
using System.Net;
using EntityFramework.Exceptions.Common;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Pfts.Api.Localization.Abstractions;
using Pfts.Domain.Exceptions;

public static class ExceptionHandlerExtensions
{
    private static readonly DefaultContractResolver ContractResolver = new()
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };

    public static IApplicationBuilder UseApplicationExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(config =>
            config.Run(async context =>
                await HandleExceptionAsync(context).ConfigureAwait(false)));

        return app;
    }

    private static async Task HandleExceptionAsync(HttpContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (contextFeature != null)
        {
            var exception = contextFeature.Error;
            var localizer = context.RequestServices.GetService<ILocalizer>();
            var statusCode = exception.GetStatusCode();
            var problemDetails = GetProblemDetails(exception, localizer, (int)statusCode);

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
            {
                ContractResolver = ContractResolver,
                Formatting = Formatting.Indented
            })).ConfigureAwait(false);

            var loggerFactory = context.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("ExceptionHandler");
            logger.LogError(exception, problemDetails?.Title);
        }
    }

    public static string GetProblemDetailsText(Exception exception, ILocalizer? localizer, int statusCode)
    {
        var problemDetails = GetProblemDetails(exception, localizer, statusCode);

        return JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings
        {
            ContractResolver = ContractResolver,
            Formatting = Formatting.Indented
        });
    }

    public static ProblemDetails GetProblemDetails(Exception exception, ILocalizer? localizer, int statusCode)
    {
        string? localizedMessage = null;

        if (exception is AppException appException && !string.IsNullOrWhiteSpace(appException.MessageFormat))
            localizedMessage = string.Format(CultureInfo.InvariantCulture, localizer?[appException.MessageFormat] ?? exception.Message, appException.Args);

        if (string.IsNullOrWhiteSpace(localizedMessage))
            localizedMessage = localizer?[exception.Message] ?? exception.Message;

        var (message, errors) = exception switch
        {
            ValidationException => (localizedMessage, ((ValidationException)exception)?.Errors?.ToDictionary()),
            _ => (localizedMessage, null),
        };

        return errors == null
            ? new ProblemDetails
            {
                Title = message,
                Status = statusCode
            }
            : new HttpValidationProblemDetails(errors)
            {
                Title = message,
                Status = statusCode,
            };
    }

    public static HttpStatusCode GetStatusCode(this Exception exception)
    {
        return exception switch
        {
            NotFoundException => HttpStatusCode.NotFound,
            AccessDeniedException => HttpStatusCode.Forbidden,
            ApplicationException => HttpStatusCode.BadRequest,
            ValidationException => HttpStatusCode.BadRequest,
            AlreadyExistsException => HttpStatusCode.Conflict,
            UniqueConstraintException => HttpStatusCode.Conflict,
            _ => HttpStatusCode.InternalServerError
        };
    }

    public static IDictionary<string, string[]> ToDictionary(this IEnumerable<ValidationFailure> errors)
    {
        return errors
          .GroupBy(x => x.PropertyName)
          .ToDictionary(
            g => g.Key,
            g => g.Select(x => x.ErrorMessage).ToArray());
    }
}
