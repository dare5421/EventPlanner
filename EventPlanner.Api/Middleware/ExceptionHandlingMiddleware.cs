using EventPlanner.Api.Models; // Ensure this matches where ErrorResponse is
using System.Text.Json;

namespace EventPlanner.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            // Proceed with the request pipeline
            await _next(context);
        }
        catch (Exception ex)
        {
            // If ANY exception occurs, catch it here
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Message = exception.Message
        };

        switch (exception)
        {
            case Application.Exceptions.UserAlreadyExistsException:
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                break;

            case Application.Exceptions.NotFoundException:
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                break;

            case Application.Exceptions.UnauthorizedException:
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                break;

            default:
                _logger.LogError(exception, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "An unexpected error occurred.";
                break;
        }

        response.StatusCode = context.Response.StatusCode;

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, jsonOptions);

        await context.Response.WriteAsync(json);
    }
}