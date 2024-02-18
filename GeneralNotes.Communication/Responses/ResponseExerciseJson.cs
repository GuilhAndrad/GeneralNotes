using GeneralNotes.Communication.Enums;

namespace GeneralNotes.Communication.Responses;
public class ResponseExerciseJson
{
    public string Name { get; set; }
    public string Type { get; set; }
    public Level Level { get; set; }
    public double Charge { get; set; }
    public int Repetitions { get; set; }
    public string Stop { get; set; }
    public string Equipment { get; set; }
    public string Details { get; set; }
}
