using FluentValidation;
using GeneralNotes.Communication.Requests;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Update;
public class UpdateWorkoutRoutineValidator : AbstractValidator<RequestWorkoutRoutineJson>
{
    public UpdateWorkoutRoutineValidator()
    {
        RuleFor(x => x).SetValidator(new WorkoutRoutineValidator());
    }
}
