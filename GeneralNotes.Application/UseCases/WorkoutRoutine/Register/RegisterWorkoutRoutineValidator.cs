using FluentValidation;
using GeneralNotes.Communication.Requests;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine.Register;
public class RegisterWorkoutRoutineValidator : AbstractValidator<RequestWorkoutRoutineJson>
{
    public RegisterWorkoutRoutineValidator()
    {
        RuleFor(wr => wr).SetValidator(new WorkoutRoutineValidator());
    }
}
