using GeneralNotes.Communication.Enums;

namespace GeneralNotes.Communication.Requests;
public class RequestDashboardJson
{
    public string Name { get; set; }
    public DayOfTheWeek DayOfTheWeek { get; set; }
}
