using GeneralNotes.Domain.Entities;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using Microsoft.EntityFrameworkCore;

namespace GeneralNotes.Infrastructure.RepositoryAccess.Repository;
public class WorkoutRoutineRepository(GeneralNotesContext context) : IWorkoutRoutineReadOnlyRepository, IWorkoutRoutineUpdateOnlyRepository, IWorkoutRoutineWriteOnlyRepository
{
    private readonly GeneralNotesContext _context = context;

    public async Task Delete(long routineId)
    {
        var rotine = await _context.WorkoutRoutines
            .FirstOrDefaultAsync(wr => wr.Id == routineId);

        if (rotine != null)
        {
            _context.WorkoutRoutines.Remove(rotine);
        }
    }

    async Task<WorkoutRoutine?> IWorkoutRoutineUpdateOnlyRepository.GetById(long routineId)
    {
        return await _context.WorkoutRoutines
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(wr => wr.Id == routineId);
    }

    async Task<WorkoutRoutine?> IWorkoutRoutineReadOnlyRepository.GetRoutinesById(long routineId)
    {
        return await _context.WorkoutRoutines
            .Include(e => e.Exercises)
            .FirstOrDefaultAsync(wr => wr.Id == routineId);
    }

    public async Task<IEnumerable<WorkoutRoutine>> GetRoutinesByUser(long userId)
    {
        return await _context.WorkoutRoutines.AsNoTracking()
            .Where(routine => routine.UserId == userId)
            .Include(e => e.Exercises)
            .ToListAsync();
    }

    public async Task<int> QuantityOfRoutines(long userId)
    {
        return await _context.WorkoutRoutines.CountAsync(wr => wr.UserId == userId);
    }

    public async Task Register(WorkoutRoutine routine)
    {
        await _context.WorkoutRoutines
            .AddAsync(routine);
    }

    public void Update(WorkoutRoutine routine)
    {
        _context.WorkoutRoutines
            .Update(routine);
    }
}
