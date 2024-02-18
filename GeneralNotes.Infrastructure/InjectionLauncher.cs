using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.User;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using GeneralNotes.Infrastructure.RepositoryAccess;
using GeneralNotes.Infrastructure.RepositoryAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralNotes.Infrastructure;
public static class InjectionLauncher
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configurationManager)
    {
        AddContext(services, configurationManager);
        AddUnitOfWork(services);
        AddRepositories(services);
    }
    private static void AddContext(IServiceCollection services, IConfiguration configurationManager)
    {
        services.AddDbContext<GeneralNotesContext>(options =>
           options.UseSqlite(configurationManager.GetConnectionString("DefaultConnection")));
    }

    private static void AddUnitOfWork(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserReadOnlyRepository, UserRepository>()
                .AddScoped<IUserUpdateOnlyRepository, UserRepository>()
                .AddScoped<IUserWriteOnlyRepository, UserRepository>()
                .AddScoped<IWorkoutRoutineReadOnlyRepository, WorkoutRoutineRepository>()
                .AddScoped<IWorkoutRoutineUpdateOnlyRepository, WorkoutRoutineRepository>()
                .AddScoped<IWorkoutRoutineWriteOnlyRepository, WorkoutRoutineRepository>();
    }
}
