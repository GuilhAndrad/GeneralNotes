namespace GeneralNotes.Domain.Repositories.WorkoutRoutine;
public interface IWorkoutRoutineUpdateOnlyRepository
{
    Task<Entities.WorkoutRoutine?> GetById(long routineId);
    void Update(Entities.WorkoutRoutine routine);
}
