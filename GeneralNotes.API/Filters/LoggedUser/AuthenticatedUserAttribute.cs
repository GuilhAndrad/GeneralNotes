using GeneralNotes.Application.Services.Token;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories.User;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace GeneralNotes.API.Filters.LoggedUser;

public class AuthenticatedUserAttribute(TokenController tokenController, IUserReadOnlyRepository userReadOnly) : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = RequestToken(context);
            var userEmail = _tokenController.RecoverEmail(token);

            var user = await _userReadOnly.RecoverByEmail(userEmail);

            if (user is null)
            {
                throw new GeneralNotesException(string.Empty);

            }
        }
        catch (SecurityTokenExpiredException)
        {
            TokenExpired(context);
        }
        catch
        {
            UserWithoutPermission(context);
        }
    }

    private static string RequestToken(AuthorizationFilterContext context)
    {
        var authorization = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrWhiteSpace(authorization))
        {
            throw new GeneralNotesException(string.Empty);
        }

        return authorization["Bearer".Length..].Trim();
    }


    private static void TokenExpired(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceExceptionsMensages.TOKEN_EXPIRADO));
    }

    private static void UserWithoutPermission(AuthorizationFilterContext context)
    {
        context.Result = new UnauthorizedObjectResult(new ResponseErrorJson(ResourceErrorMensages.USUARIO_SEM_PERMISSAO));
    }

}
