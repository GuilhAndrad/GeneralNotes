using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.User.Register;
public interface IRegisterUserUseCase
{
    Task<ResponseRegisteredUserJson> ExecuteInsertion(RequestRegisterUserJson requestUser);
}
