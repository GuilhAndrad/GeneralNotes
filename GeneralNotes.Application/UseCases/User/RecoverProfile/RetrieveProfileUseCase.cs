using AutoMapper;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.User.RecoverProfile;
public class RetrieveProfileUseCase(IMapper mapper, ILoggedUser loggedUser) : IRetrieveProfileUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;

    public async Task<ResponseUserProfileJson> RecoverUser()
    {
        var user = await _loggedUser.RecoverUser();

        return _mapper.Map<ResponseUserProfileJson>(user);
    }
}
