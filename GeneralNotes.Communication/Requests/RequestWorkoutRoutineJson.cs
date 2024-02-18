using GeneralNotes.Communication.Enums;

namespace GeneralNotes.Communication.Requests;
public class RequestWorkoutRoutineJson
{
    public RequestWorkoutRoutineJson()
    {
        Exercises = [];
    }
    public string Name { get; set; }
    public DayOfTheWeek DayOfWeek { get; set; }
    public List<RequestExerciseJson> Exercises { get; set; }
}
