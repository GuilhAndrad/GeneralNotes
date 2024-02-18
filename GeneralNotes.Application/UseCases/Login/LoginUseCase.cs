using GeneralNotes.Application.Services.Cryptography;
using GeneralNotes.Application.Services.Token;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories.User;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.Login;
public class LoginUseCase(IUserReadOnlyRepository userReadOnlyRepository,
    PasswordEncryptor passwordEncryptor,
    TokenController tokenController) : ILoginUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository = userReadOnlyRepository;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly TokenController _tokenController = tokenController;

    public async Task<ResponseLoginJson> ExecuteLogin(RequestLoginJson requestLogin)
    {
        var passwordEncrypted = _passwordEncryptor.Encrypt(requestLogin.Password);

        var user = await _userReadOnlyRepository.RecoverByEmailPassword(requestLogin.Email, passwordEncrypted);
        if (user is null)
        {
            throw new LoginInvalidException();
        }

        return new ResponseLoginJson
        {
            Name = user.Name,
            Token = _tokenController.GenerateToken(user.Email)
        };
    }
}
