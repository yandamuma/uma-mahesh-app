using Microsoft.AspNetCore.Diagnostics;
namespace UmaMahesh_BackApp.Exceptions;

public class GeneralExceptionHandler(ILogger<GeneralExceptionHandler> logger) : IExceptionHandler
{    
   
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        (int statusCode, string errorMessage) = exception switch 
        {
            _ => (StatusCodes.Status500InternalServerError, "We made a mistake but we are on it!")
        };

        logger.LogError(statusCode, errorMessage);
        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsync(errorMessage, cancellationToken).ConfigureAwait(false);

        return true;
    }
}
