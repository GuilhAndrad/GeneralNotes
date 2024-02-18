using GeneralNotes.Application.Services.Token;
using GeneralNotes.Domain.Repositories.User;
using Microsoft.AspNetCore.Authorization;

namespace GeneralNotes.API.Filters.LoggedUser;

public class LoggedUserHandler(IHttpContextAccessor contextAccessor, TokenController tokenController, IUserReadOnlyRepository userReadOnly) : AuthorizationHandler<LoggedUserRequirement>
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, LoggedUserRequirement requirement)
    {
        try
        {
            var authorization = _contextAccessor.HttpContext?.Request.Headers?.Authorization.ToString();

            if (string.IsNullOrWhiteSpace(authorization))
            {
                context.Fail();
                return;
            }

            var token = authorization["Bearer".Length..].Trim();

            var userEmail = _tokenController.RecoverEmail(token);

            if (string.IsNullOrWhiteSpace(userEmail))
            {
                context.Fail();
                return;
            }
            var user = await _userReadOnly.RecoverByEmail(userEmail);


            if (user is null)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
        catch
        {

            context.Fail();
        }
    }
}