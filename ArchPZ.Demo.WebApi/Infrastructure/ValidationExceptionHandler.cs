namespace ArchPZ.Demo.WebApi.Infrastructure;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ArchPZ.Demo.Application.Common.Exceptions;

public class ValidationExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ValidationException validationException)
            return false;

        var problemDetails = new ValidationProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Title = "Помилка валідації.",
            Detail = "Одне або кілька полів містять недійсні дані.",
            Instance = httpContext.Request.Path,
            Errors = validationException.Errors.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value)
        };

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
