namespace GeneralNotes.Exceptions.ExceptionsBase;
public class NotFoundException(List<string> errors) : GeneralNotesException(string.Empty)
{
    public List<string> Errors { get; set; } = errors;
}
