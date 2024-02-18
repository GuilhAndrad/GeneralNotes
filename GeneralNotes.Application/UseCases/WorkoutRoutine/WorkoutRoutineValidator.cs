using FluentValidation;
using GeneralNotes.Communication.Requests;
using GeneralNotes.Exceptions;

namespace GeneralNotes.Application.UseCases.WorkoutRoutine;
public class WorkoutRoutineValidator : AbstractValidator<RequestWorkoutRoutineJson>
{
    public WorkoutRoutineValidator()
    {
        RuleFor(wr => wr.Name).NotEmpty().WithMessage(ResourceErrorMensages.NOME_ROTINA_EMBRANCO);
        RuleFor(wr => wr.DayOfWeek).IsInEnum();
        RuleFor(wr => wr.Exercises).NotEmpty().WithMessage(ResourceErrorMensages.EXERCÍCIOS_EMBRANCO);
        RuleForEach(wr => wr.Exercises).ChildRules(exercices =>
        {
            exercices.RuleFor(e => e.Name).NotEmpty().WithMessage(ResourceErrorMensages.NOME_EXERCÍCIO_EMBRANCO);
            exercices.RuleFor(e => e.Type).NotEmpty().WithMessage(ResourceErrorMensages.TIPO_EXERCÍCIO_EMBRANCO);
            exercices.RuleFor(e => e.Level).IsInEnum();
            exercices.RuleFor(e => e.Charge).NotEmpty().WithMessage(ResourceErrorMensages.PESO_EXERCÍCIO_EMBRANCO);
            exercices.RuleFor(e => e.Repetitions).NotEmpty().WithMessage(ResourceErrorMensages.REPERTICOES_EXERCÍCIO_EMBRANCO);
            exercices.RuleFor(e => e.Stop).NotEmpty().WithMessage(ResourceErrorMensages.TEMPO_ESPERA_EMBRANCO);
            exercices.RuleFor(e => e.Equipment).NotEmpty().WithMessage(ResourceErrorMensages.EQUIPAMENTO_EXERCÍCIO_EMBRANCO);
            exercices.RuleFor(e => e.Details).NotEmpty().WithMessage(ResourceErrorMensages.DETALHE_EXERCÍCIO_EMBRANCO);
        });
    }
}
