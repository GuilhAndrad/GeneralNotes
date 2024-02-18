using AutoMapper;

namespace GeneralNotes.Application.Services.Automapper;
public class AutoMapperConfiguration : Profile
{
    public AutoMapperConfiguration()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<Communication.Requests.RequestRegisterUserJson, Domain.Entities.User>();
        CreateMap<Communication.Requests.RequestWorkoutRoutineJson, Domain.Entities.WorkoutRoutine>()
            .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.Exercises));
        CreateMap<Communication.Requests.RequestExerciseJson, Domain.Entities.Exercise>();

    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.User, Communication.Responses.ResponseUserProfileJson>();
        CreateMap<Domain.Entities.WorkoutRoutine, Communication.Responses.ResponseWorkoutRoutineJson>();
        CreateMap<Domain.Entities.Exercise, Communication.Responses.ResponseExerciseJson>();
        CreateMap<Domain.Entities.WorkoutRoutine, Communication.Responses.ResponseRoutinesDashboardJson>()
            .ForMember(dest => dest.ExerciseCount, config => config.MapFrom(ori => ori.Exercises.Count));
    }
}
