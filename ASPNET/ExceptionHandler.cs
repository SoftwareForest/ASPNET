using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET;

public class ExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var detail = new ProblemDetails();
        if (exception is InvalidOperationException invalidException)
        {
            detail = new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Detail = invalidException.Message,
            };

            detail.Extensions["Errors"] = new List<string>() { "Error 1", "Error 2" };
        }
        httpContext.Response.StatusCode = detail.Status.Value;
        await httpContext.Response.WriteAsJsonAsync(detail);

        return true;
    }
}
