using GeneralNotes.API.Filters.LoggedUser;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Delete;
using GeneralNotes.Application.UseCases.WorkoutRoutine.GetId;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Register;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Update;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GeneralNotes.API.Controllers;
[Route("api/[controller]")]
[ApiController]
[ServiceFilter(typeof(AuthenticatedUserAttribute))]
public class WorkoutRoutinesController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseWorkoutRoutineJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterWorkoutRoutine(
    [FromServices] IRegisterWorkoutRoutineUseCase wrUseCase,
    [FromBody] RequestWorkoutRoutineJson requestWr)
    {
        var response = await wrUseCase.ExecuteInsertion(requestWr);

        return Created(string.Empty, response);
    }

    [HttpGet("{routineId}")]
    [ProducesResponseType(typeof(ResponseWorkoutRoutineJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetWorkoutRoutineById(
        [FromServices] IGetWorkoutRoutineByIdUseCase routineUseCase,
        [FromRoute] long routineId)
    {
        var response = await routineUseCase.GetWorkoutRoutineByIdAsync(routineId);
        return Ok(response);
    }

    [HttpPut]
    [Route("{routineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateWorkouRoutine(
    [FromServices] IUpdateWorkoutRoutineUseCase routineUseCase,
    [FromBody] RequestWorkoutRoutineJson requestRoutine,
    [FromRoute] long routineId)
    {
        await routineUseCase.UpdateWorkoutRoutineAsync(routineId, requestRoutine);

        return NoContent();
    }

    [HttpDelete]
    [Route("{routineId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteWorkoutRoutine(
        [FromServices] IDeleteWorkoutRoutineUseCase routineUseCase,
        [FromRoute] long routineId)
    {
        await routineUseCase.Delete(routineId);
        return NoContent();
    }
}
