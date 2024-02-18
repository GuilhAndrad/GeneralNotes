namespace GeneralNotes.Domain.Repositories.WorkoutRoutine;
public interface IWorkoutRoutineReadOnlyRepository
{
    Task<IEnumerable<Entities.WorkoutRoutine>> GetRoutinesByUser(long userId);
    Task<Entities.WorkoutRoutine?> GetRoutinesById(long routineId);
    Task<int> QuantityOfRoutines(long userId);
}
