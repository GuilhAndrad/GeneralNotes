using GeneralNotes.Domain.Entities;
using GeneralNotes.Infrastructure.TypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace GeneralNotes.Infrastructure.RepositoryAccess;
public class GeneralNotesContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<WorkoutRoutine> WorkoutRoutines { get; set; }
    public DbSet<Exercise> Exercise { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new WorkoutRoutineConfiguration());
    }
}
