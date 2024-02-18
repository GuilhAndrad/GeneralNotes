namespace GeneralNotes.Domain.Repositories.WorkoutRoutine;
public interface IWorkoutRoutineWriteOnlyRepository
{
    Task Register(Entities.WorkoutRoutine routine);
    Task Delete(long routineId);
}
