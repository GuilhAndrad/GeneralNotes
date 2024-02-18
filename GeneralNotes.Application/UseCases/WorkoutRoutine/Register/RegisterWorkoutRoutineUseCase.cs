using AutoMapper;
using GeneralNotes.Application.Services.UserLogged;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;
using GeneralNotes.Domain.Repositories;
using GeneralNotes.Domain.Repositories.WorkoutRoutine;
using GeneralNotes.Exceptions.ExceptionsBase;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Register;
public class RegisterWorkoutRoutineUseCase(IMapper mapper, IUnitOfWork unitOfWork, ILoggedUser logged, IWorkoutRoutineWriteOnlyRepository wrOnlyRepository) : IRegisterWorkoutRoutineUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly ILoggedUser _logged = logged;
    private readonly IWorkoutRoutineWriteOnlyRepository _wrOnlyRepository = wrOnlyRepository;
    public async Task<ResponseWorkoutRoutineJson> ExecuteInsertion(RequestWorkoutRoutineJson requestWorkoutRoutineJson)
    {
        Validate(requestWorkoutRoutineJson);

        var loggedUser = await _logged.RecoverUser();

        var workoutRoutine = _mapper.Map<Domain.Entities.WorkoutRoutine>(requestWorkoutRoutineJson);
        workoutRoutine.UserId = loggedUser.Id;

        await _wrOnlyRepository.Register(workoutRoutine);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseWorkoutRoutineJson>(workoutRoutine);
    }

    private static void Validate(RequestWorkoutRoutineJson requestWorkoutRoutineJson)
    {
        var validator = new RegisterWorkoutRoutineValidator();
        var result = validator.Validate(requestWorkoutRoutineJson);

        if (!result.IsValid)
        {
            var errorMensage = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ValidationErrorException(errorMensage);
        }
    }
}
