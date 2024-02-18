using GeneralNotes.Communication.Responses;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GeneralNotes.API.Filters;

public class ExceptionFilters : IExceptionFilter
{
    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers;

    public ExceptionFilters()
    {
        _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
        {
            { typeof(ValidationErrorException), HandleValidationErrorException },
            { typeof(NotFoundException), HandleNotFoundException },
            { typeof(LoginInvalidException), HandleLoginException },
        };
    }

    public void OnException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out Action<ExceptionContext> value))
        {
            value.Invoke(context);
        }
        else
        {
            HandleUnknownException(context);
        }
    }

    private void HandleValidationErrorException(ExceptionContext context)
    {
        if (context.Exception is ValidationErrorException exception)
        {
            SetResponse(context, HttpStatusCode.BadRequest, exception.ErrorMensage);
        }
    }

    private void HandleNotFoundException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException exception)
        {
            SetResponse(context, HttpStatusCode.NotFound, exception.Errors);
        }
    }

    private static void HandleUnknownException(ExceptionContext context)
    {
        var errorResponse = new ResponseErrorJson(new List<string> { ResourceExceptionsMensages.INTERNAL_SERVER_ERROR, context.Exception.Message });
        SetResponse(context, HttpStatusCode.InternalServerError, errorResponse);
    }

    private static void HandleLoginException(ExceptionContext context)
    {
        if (context.Exception is LoginInvalidException errorLogin)
        {
            var response = new ResponseErrorJson(new List<string> { errorLogin.Message });
            SetResponse(context, HttpStatusCode.Unauthorized, response);
        }
    }

    private static void SetResponse(ExceptionContext context, HttpStatusCode statusCode, object response)
    {
        context.HttpContext.Response.StatusCode = (int)statusCode;
        context.Result = new ObjectResult(response)
        {
            StatusCode = (int)statusCode
        };
    }
}
