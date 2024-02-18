using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.Login;
public interface ILoginUseCase
{
    Task<ResponseLoginJson> ExecuteLogin(RequestLoginJson requestLogin);
}
