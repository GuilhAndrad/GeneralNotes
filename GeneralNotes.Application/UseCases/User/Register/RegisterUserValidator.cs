using FluentValidation;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Exceptions;
using RegistroDeFretes.Application.UseCases.User;
using System.Text.RegularExpressions;

namespace GeneralNotes.Application.UseCases.User.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public RegisterUserValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage(ResourceErrorMensages.NOME_USUARIO_EMBRANCO);
        RuleFor(c => c.Email).NotEmpty().WithMessage(ResourceErrorMensages.EMAIL_USUARIO_EMBRANCO);
        RuleFor(c => c.Password).SetValidator(new PasswordValidator());
        When(c => !string.IsNullOrWhiteSpace(c.Email), () =>
        {
            RuleFor(c => c.Email).EmailAddress().WithMessage(ResourceErrorMensages.EMAIL_USUARIO_INVALIDO);
            RuleFor(c => c.Email).Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage(ResourceErrorMensages.EMAIL_USUARIO_CARACTERES_INVALIDOS);
        });

        When(c => !string.IsNullOrWhiteSpace(c.Contact), () =>
        {
            RuleFor(c => c.Contact).Custom((contact, context) =>
            {
                string defaultContact = "[0-9]{2} [1-9]{1} [0-9]{4}-[0-9]{4}";
                var isMatch = Regex.IsMatch(contact, defaultContact);
                if (!isMatch)
                {
                    context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(contact), ResourceErrorMensages.FORMATO_TELEFONE_INVALIDO));
                }
            });
        });
    }
}
