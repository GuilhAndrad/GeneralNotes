using AutoMapper;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.GetId;
public class GetWorkoutRoutineByIdUseCase(IMapper mapper, IWorkoutRoutineReadOnlyRepository readOnlyRepository, ILoggedUser logged) : IGetWorkoutRoutineByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IWorkoutRoutineReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly ILoggedUser _logged = logged;

    public async Task<ResponseWorkoutRoutineJson> GetWorkoutRoutineByIdAsync(long routineId)
    {
        var loggedUser = await _logged.RecoverUser();
        var routine = await _readOnlyRepository.GetRoutinesById(routineId);

        Validate(loggedUser, routine);

        return _mapper.Map<ResponseWorkoutRoutineJson>(routine);

    }

    private static void Validate(Domain.Entities.User loggedUser, Domain.Entities.WorkoutRoutine routine)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
        {
            throw new NotFoundException([ResourceErrorMensages.ROTINA_NAO_ENCONTRADA]);
        }
    }
}
