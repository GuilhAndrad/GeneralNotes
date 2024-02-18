using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.GetId;
public interface IGetWorkoutRoutineByIdUseCase
{
    Task<ResponseWorkoutRoutineJson> GetWorkoutRoutineByIdAsync(long routineId);
}
