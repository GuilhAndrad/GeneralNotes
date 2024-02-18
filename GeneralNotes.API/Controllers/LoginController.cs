using GeneralNotes.Application.UseCases.Login;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeneralNotes.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseLoginJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase loginUseCase,
        [FromBody] RequestLoginJson loginRequest)
    {
        var response = await loginUseCase.ExecuteLogin(loginRequest);

        return Ok(response);
    }
}
