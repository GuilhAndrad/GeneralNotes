using FluentValidation;
using GeneralNotes.Exceptions;

namespace RegistroDeFretes.Application.UseCases.User;
public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password).NotEmpty().WithMessage(ResourceErrorMensages.SENHA_USUARIO_EMBRANCO);
        When(password => !string.IsNullOrWhiteSpace(password), () =>
        {
            RuleFor(password => password.Length).GreaterThanOrEqualTo(8).WithMessage(ResourceErrorMensages.SENHA_USUARIO_MINIMO_OITO_CARACTERES);
            RuleFor(password => password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$").WithMessage(ResourceErrorMensages.SENHA_USUARIO_CARACTERES_INVALIDOS);
        });
    }
}
