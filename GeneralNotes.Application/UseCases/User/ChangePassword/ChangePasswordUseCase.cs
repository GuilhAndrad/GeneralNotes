using GeneralNotes.Application.Services.Cryptography;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.User;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.User.ChangePassword;
public class ChangePasswordUseCase(ILoggedUser loggedUser,
    IUserUpdateOnlyRepository updateOnlyRepository,
    PasswordEncryptor passwordEncryptor,
    IUnitOfWork unitOfWork) : IChangePasswordUseCase
{
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IUserUpdateOnlyRepository _updateOnlyRepository = updateOnlyRepository;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task ExecuteChange(RequestChangePasswordJson requestChange)
    {
        var loggedUser = await _loggedUser.RecoverUser();
        var user = await _updateOnlyRepository.GetById(loggedUser.Id);

        Validate(requestChange, user);

        user.Password = _passwordEncryptor.Encrypt(requestChange.NewPassword);
        _updateOnlyRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson requestChange, Domain.Entities.User user)
    {
        var validator = new ChangePasswordValidator();
        var result = validator.Validate(requestChange);

        var currentPasswordEncrypted = _passwordEncryptor.Encrypt(requestChange.CurrentPassword);

        if (!user.Password.Equals(currentPasswordEncrypted))
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("senhaAtual", ResourceErrorMensages.SENHA_ATUAL_INVALIDA));
        }
        if (!result.IsValid)
        {
            var errorMensages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMensages);
        }
    }
}
