using GeneralNotes.Communication.Requests;

namespace GeneralNotes.Application.UseCases.User.ChangePassword;
public interface IChangePasswordUseCase
{
    Task ExecuteChange(RequestChangePasswordJson requestChange);
}
