using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using VoucherManagementSystem.Application.Common.Models;
using System.Net;
using System.Text.Json;

namespace VoucherManagementSystem.API.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var message = "An error occurred while processing your request.";

        // Handle specific exception types
        switch (exception)
        {
            case ValidationException validationEx:
                code = HttpStatusCode.BadRequest;
                message = string.Join(" | ", validationEx.Errors.Select(e => e.ErrorMessage));
                break;
            case ArgumentException argEx:
                code = HttpStatusCode.BadRequest;
                message = argEx.Message;
                break;

            case KeyNotFoundException:
                code = HttpStatusCode.NotFound;
                message = "The requested resource was not found.";
                break;

            case UnauthorizedAccessException:
                code = HttpStatusCode.Unauthorized;
                message = "You do not have permission to access this resource.";
                break;

            case InvalidOperationException invalidOpEx:
                code = HttpStatusCode.BadRequest;
                message = invalidOpEx.Message;
                break;

            case TimeoutException:
                code = HttpStatusCode.RequestTimeout;
                message = "The request timed out.";
                break;
        }

        var result = JsonSerializer.Serialize(new
        {
            isSuccess = false,
            error = message,
            value = (object?)null
        });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        return context.Response.WriteAsync(result);
    }
}
