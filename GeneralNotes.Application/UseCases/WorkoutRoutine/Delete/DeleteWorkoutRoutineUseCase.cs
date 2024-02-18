
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using GeneralNotes.Exceptions;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Delete;
public class DeleteWorkoutRoutineUseCase(IWorkoutRoutineWriteOnlyRepository writeOnlyRepository, IWorkoutRoutineReadOnlyRepository readOnlyRepository, ILoggedUser logged, IUnitOfWork unitOfWork) : IDeleteWorkoutRoutineUseCase
{
    private readonly IWorkoutRoutineWriteOnlyRepository _writeOnlyRepository = writeOnlyRepository;
    private readonly IWorkoutRoutineReadOnlyRepository _readOnlyRepository = readOnlyRepository;
    private readonly ILoggedUser _logged = logged;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task Delete(long routineId)
    {
        var loggedUser = await _logged.RecoverUser();

        var routine = await _readOnlyRepository.GetRoutinesById(routineId);

        Validate(loggedUser, routine);

        await _writeOnlyRepository.Delete(routineId);

        await _unitOfWork.Commit();
    }

    private static void Validate(Domain.Entities.User loggedUser, Domain.Entities.WorkoutRoutine routine)
    {
        if (routine is null || routine.UserId != loggedUser.Id)
        {
            throw new NotFoundException([ResourceErrorMensages.ROTINA_NAO_ENCONTRADA]);
        }
    }
}
