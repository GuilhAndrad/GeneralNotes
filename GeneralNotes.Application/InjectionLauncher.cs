using GeneralNotes.Application.Services.Cryptography;
using GeneralNotes.Application.Services.Token;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Application.UseCases.Dashboard;
using GeneralNotes.Application.UseCases.Login;
using GeneralNotes.Application.UseCases.User.ChangePassword;
using GeneralNotes.Application.UseCases.User.RecoverProfile;
using GeneralNotes.Application.UseCases.User.Register;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Delete;
using GeneralNotes.Application.UseCases.WorkoutRoutine.GetId;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Register;
using GeneralNotes.Application.UseCases.WorkoutRoutine.Update;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeneralNotes.Application;
public static class InjectionLauncher
{
    public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        AddUseCases(services);
        AddAdditionalKeyPassword(services, configuration);
        AddTokenJwt(services, configuration);
        AddLoggedUser(services);
    }

    private static void AddLoggedUser(this IServiceCollection services)
    {
        services.AddScoped<ILoggedUser, LoggedUser>();
    }

    private static void AddAdditionalKeyPassword(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetRequiredSection("Configurations:Password:AdditionalPasswordKey");

        services.AddScoped(options => new PasswordEncryptor(section.Value));
    }

    private static void AddTokenJwt(IServiceCollection services, IConfiguration configuration)
    {
        var sectionLifeTime = configuration.GetRequiredSection("Configurations:JwtToken:LifeTimeTokenMinutes");
        var sectionKey = configuration.GetRequiredSection("Configurations:JwtToken:TokenKey");

        services.AddScoped(options => new TokenController(int.Parse(sectionLifeTime.Value), sectionKey.Value));
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>()
                .AddScoped<ILoginUseCase, LoginUseCase>()
                .AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>()
                .AddScoped<IRetrieveProfileUseCase, RetrieveProfileUseCase>()
                .AddScoped<IRegisterWorkoutRoutineUseCase, RegisterWorkoutRoutineUseCase>()
                .AddScoped<IGetWorkoutRoutineByIdUseCase, GetWorkoutRoutineByIdUseCase>()
                .AddScoped<IUpdateWorkoutRoutineUseCase, UpdateWorkoutRoutineUseCase>()
                .AddScoped<IDeleteWorkoutRoutineUseCase, DeleteWorkoutRoutineUseCase>()
                .AddScoped<IDashboardUseCase, DashboardUseCase>();
    }

}
