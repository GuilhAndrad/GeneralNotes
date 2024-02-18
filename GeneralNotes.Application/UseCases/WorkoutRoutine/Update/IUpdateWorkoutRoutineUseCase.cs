using GeneralNotes.Communication.Requests;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Update;
public interface IUpdateWorkoutRoutineUseCase
{
    Task UpdateWorkoutRoutineAsync(long routineId, RequestWorkoutRoutineJson request);
}
