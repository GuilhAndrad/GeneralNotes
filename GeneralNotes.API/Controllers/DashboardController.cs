using GeneralNotes.API.Filters.LoggedUser;
using GeneralNotes.Application.UseCases.Dashboard;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeneralNotes.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class DashboardController : ControllerBase
{
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDashboardJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetDashboard(
        [FromServices] IDashboardUseCase dashboardUse,
        [FromBody] RequestDashboardJson request)
    {
        var result = await dashboardUse.GetDashboard(request);
        if (result.WorkoutRoutines.Count != 0)
        {
            return Ok(result);
        }
        return NoContent();
    }
}