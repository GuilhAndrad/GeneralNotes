using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.User.RecoverProfile;
public interface IRetrieveProfileUseCase
{
    Task<ResponseUserProfileJson> RecoverUser();

}
