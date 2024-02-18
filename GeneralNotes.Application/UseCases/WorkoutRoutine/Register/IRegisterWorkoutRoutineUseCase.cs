using GeneralNotes.Communication.Requests;
using GeneralNotes.Communication.Responses;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Register;
public interface IRegisterWorkoutRoutineUseCase
{
    Task<ResponseWorkoutRoutineJson> ExecuteInsertion(RequestWorkoutRoutineJson requestWorkoutRoutineJson);
}
