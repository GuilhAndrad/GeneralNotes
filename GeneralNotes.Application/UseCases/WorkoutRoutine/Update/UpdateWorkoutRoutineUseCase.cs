using AutoMapper;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Update;
public class UpdateWorkoutRoutineUseCase(IMapper mapper, IWorkoutRoutineUpdateOnlyRepository updateOnlyRepository, ILoggedUser logged, IUnitOfWork unitOfWork) : IUpdateWorkoutRoutineUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IWorkoutRoutineUpdateOnlyRepository _updateOnlyRepository = updateOnlyRepository;
    private readonly ILoggedUser _logged = logged;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task UpdateWorkoutRoutineAsync(long routineId, RequestWorkoutRoutineJson request)
    {
        var loggedUser = await _logged.RecoverUser();
        var routine = await _updateOnlyRepository.GetById(routineId);

        Validate(loggedUser, routine, request);

        _mapper.Map(request, routine);

        _updateOnlyRepository.Update(routine);

        await _unitOfWork.Commit();
    }

    private static void Validate(Domain.Entities.User loggedUser, Domain.Entities.WorkoutRoutine routine, RequestWorkoutRoutineJson request)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
        {
            throw new NotFoundException([ResourceErrorMensages.ROTINA_NAO_ENCONTRADA]);
        }

        var validator = new UpdateWorkoutRoutineValidator();
        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ValidationErrorException(errors);
        }
    }
}
