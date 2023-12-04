using Microsoft.AspNetCore.Diagnostics;
namespace UmaMahesh_BackApp.Exceptions;

public class AppExceptionHandler(ILogger<AppExceptionHandler> logger) : IExceptionHandler
{   
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch 
        {
            BadHttpRequestException badHttpRequestException => (StatusCodes.Status400BadRequest, badHttpRequestException.Message),
            KeyNotFoundException keyNotFoundException => (StatusCodes.Status404NotFound, keyNotFoundException.Message),
            NotImplementedException notImplementedException => (StatusCodes.Status501NotImplemented, notImplementedException.Message),
            NullReferenceException nullReferenceException => (StatusCodes.Status406NotAcceptable, nullReferenceException.Message),
            ArgumentOutOfRangeException argumentOutOfRangeException => (StatusCodes.Status400BadRequest, argumentOutOfRangeException.Message),
            UnauthorizedAccessException unauthorizedAccessException => (StatusCodes.Status401Unauthorized,unauthorizedAccessException.Message),
            _ => default
        };

        if (statusCode == default)
        {
            return false;
        }

        logger.LogError(statusCode, errorMessage);

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(errorMessage);

      return true;
    }
}
