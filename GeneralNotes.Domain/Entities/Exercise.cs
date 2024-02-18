using GeneralNotes.Domain.Enums;

namespace GeneralNotes.Domain.Entities;
public class Exercise : BaseEntity
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Level Level { get; set; }
    public double Charge { get; set; }
    public int Repetitions { get; set; }
    public string Stop { get; set; }
    public string Equipment { get; set; }
    public string Details { get; set; }
    public long WorkoutRoutineId { get; set; }
}
