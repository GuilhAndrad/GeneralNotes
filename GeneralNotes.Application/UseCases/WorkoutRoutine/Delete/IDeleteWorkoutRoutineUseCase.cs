namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Delete;
public interface IDeleteWorkoutRoutineUseCase
{
    Task Delete(long routineId);
}
