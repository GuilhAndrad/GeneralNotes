using GeneralNotes.API.Filters.LoggedUser;
using GeneralNotes.Application.UseCases.User.ChangePassword;
using GeneralNotes.Application.UseCases.User.RecoverProfile;
using GeneralNotes.Application.UseCases.User.Register;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeneralNotes.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterUser(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson userRequest)
    {
        var result = await useCase.ExecuteInsertion(userRequest);

        return Created(string.Empty, result);
    }

    [HttpPatch]
    [Route("alterar-senha")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> ChangePassword(
            [FromServices] IChangePasswordUseCase changeUseCase,
            [FromBody] RequestChangePasswordJson changeRequest)
    {
        await changeUseCase.ExecuteChange(changeRequest);

        return NoContent();
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseUserProfileJson), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(AuthenticatedUserAttribute))]
    public async Task<IActionResult> GetUserProfile(
        [FromServices] IRetrieveProfileUseCase profileUseCase)
    {
        var result = await profileUseCase.RecoverUser();

        return Ok(result);
    }
}
