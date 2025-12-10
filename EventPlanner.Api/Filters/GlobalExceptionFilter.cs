using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventPlanner.Api.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;

        int statusCode;
        string message;

        switch (context.Exception)
        {
            case Application.Exceptions.NotFoundException notFoundException:
                statusCode = StatusCodes.Status404NotFound;
                message = notFoundException.Message;
                break;
            case Application.Exceptions.UnauthorizedException unauthorizedException:
                statusCode = StatusCodes.Status401Unauthorized;
                message = unauthorizedException.Message;
                break;
            default:
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                break;
        }

        var errorResponse = new Models.ErrorResponse
        {
            StatusCode = statusCode,
            Message = message
        };
        
        context.Result = new ObjectResult(errorResponse)
        {
            StatusCode = statusCode
        };
    }
}
