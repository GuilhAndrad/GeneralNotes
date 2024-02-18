using AutoMapper;
using GeneralNotes.Application.Services.Cryptography;
using GeneralNotes.Application.Services.Token;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.User;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.User.Register;
public class RegisterUserUseCase(IMapper mapper, IUserReadOnlyRepository userReadOnly,
    IUserWriteOnlyRepository userWriteOnly,
    IUnitOfWork unitOfWork,
    PasswordEncryptor passwordEncryptor,
    TokenController tokenController) : IRegisterUserUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUserReadOnlyRepository _userReadOnly = userReadOnly;
    private readonly IUserWriteOnlyRepository _userWriteOnly = userWriteOnly;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly PasswordEncryptor _passwordEncryptor = passwordEncryptor;
    private readonly TokenController _tokenController = tokenController;

    public async Task<ResponseRegisteredUserJson> ExecuteInsertion(RequestRegisterUserJson requestUser)
    {
        await Validate(requestUser);

        var entity = _mapper.Map<Domain.Entities.User>(requestUser);
        entity.Password = _passwordEncryptor.Encrypt(requestUser.Password);

        await _userWriteOnly.Add(entity);

        await _unitOfWork.Commit();

        var token = _tokenController.GenerateToken(entity.Email);
        return new ResponseRegisteredUserJson
        {
            Token = token,
        };
    }

    public async Task Validate(RequestRegisterUserJson requestUser)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(requestUser);

        var existsUserWithEmail = await _userReadOnly.ExistUserWithEmail(requestUser.Email);
        if (existsUserWithEmail)
        {
            result.Errors.Add(new FluentValidation.Results.ValidationFailure("email", ResourceErrorMensages.EMAIL_JA_REGISTRADO));
        }

        if (!result.IsValid)
        {
            var errorMensages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMensages);
        }
    }
}