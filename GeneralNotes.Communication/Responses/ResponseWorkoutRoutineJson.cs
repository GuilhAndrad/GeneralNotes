using GeneralNotes.Communication.Enums;

namespace GeneralNotes.Communication.Responses;
public class ResponseWorkoutRoutineJson
{

    public string Name { get; set; }
    public DayOfTheWeek DayOfWeek { get; set; }
    public List<ResponseExerciseJson> Exercises { get; set; }
}
