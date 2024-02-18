using GeneralNotes.Communication.Enums;

namespace GeneralNotes.Communication.Responses;
public class ResponseRoutinesDashboardJson
{
    public string Name { get; set; }
    public DayOfTheWeek DayOfWeek { get; set; }
    public int ExerciseCount { get; set; }
}

