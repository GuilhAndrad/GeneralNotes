using GeneralNotes.Domain.Enums;

namespace GeneralNotes.Domain.Entities;
public class WorkoutRoutine : BaseEntity
{
    public string Name { get; set; }
    public DayOfTheWeek DayOfWeek { get; set; }
    public List<Exercise> Exercises { get; set; }
    public long UserId { get; set; }
}

