using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using RentAThing.Server.Application.Exceptions;

namespace RentAThing.Server.Infrastructure;
public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : IExceptionFilter {
    public void OnException(ExceptionContext context) {
        // Log the exception
        logger.LogError(context.Exception, "Unhandled exception occurred.");

        // Check if it's a custom exception
        if (context.Exception is RentException) {
            context.Result = new JsonResult(new {
                error = "An unexpected application error occurred",
                details = context.Exception.Message
            }) {
                StatusCode = StatusCodes.Status400BadRequest
            };
        } else {
            context.Result = new JsonResult(new {
                error = "An unexpected error occurred",
                details = context.Exception.Message
            }) {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        context.ExceptionHandled = true; // Mark exception as handled
    }
}
