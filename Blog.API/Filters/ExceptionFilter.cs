using System.Net;
using Blog.Domain.Communication.Responses;
using Blog.Exceptions.ExceptionMessages;
using Blog.Exceptions.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Blog.API.Filters;

public class ExceptionFilter : IExceptionFilter {
    public void OnException(ExceptionContext context) {
        if (context.Exception is BlogException blogException) {
            HandleBlogException(context, blogException);
        } else {
            HandleUnknownException(context);
        }
    }

    private static void HandleBlogException(ExceptionContext context, BlogException exception) {
        context.HttpContext.Response.StatusCode = (int)exception.GetStatusCode();
        context.Result = new ObjectResult(new ErrorResponse(exception.GetErrorMessages()));
    }

    private static void HandleUnknownException(ExceptionContext context) {
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(new ErrorResponse(ExceptionMessages.UnknownException));
    }
}