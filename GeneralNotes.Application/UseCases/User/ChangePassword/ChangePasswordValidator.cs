using FluentValidation;
using GeneralNotes.Communication.Requests;
using RegistroDeFretes.Application.UseCases.User;

namespace GeneralNotes.Application.UseCases.User.ChangePassword;
public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(c => c.NewPassword).SetValidator(new PasswordValidator());
    }
}
