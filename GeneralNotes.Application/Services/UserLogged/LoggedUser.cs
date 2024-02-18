using GeneralNotes.Application.Services.Token;
using GeneralNotes.Domain.Entities;
using GeneralNotes.Domain.Repositories.User;
using Microsoft.AspNetCore.Http;

namespace GeneralNotes.Application.Services.UserLogged;
public class LoggedUser(IHttpContextAccessor httpContextAccessor,
    TokenController tokenController,
    IUserReadOnlyRepository readOnlyRepositorio) : ILoggedUser
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly TokenController _tokenController = tokenController;
    private readonly IUserReadOnlyRepository _readOnlyRepositorio = readOnlyRepositorio;

    public async Task<User> RecoverUser()
    {
        var authorization = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        var token = authorization["Bearer".Length..].Trim();

        var emailUsuario = _tokenController.RecoverEmail(token);

        var user = await _readOnlyRepositorio.RecoverByEmail(emailUsuario);

        return user;
    }
}
