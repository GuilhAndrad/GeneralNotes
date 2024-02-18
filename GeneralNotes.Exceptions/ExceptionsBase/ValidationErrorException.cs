namespace GeneralNotes.Exceptions.ExceptionsBase;
public class ValidationErrorException : GeneralNotesException
{
    public List<string> ErrorMensage { get; set; }

    public ValidationErrorException(List<string> errorMensage) : base(string.Empty)
    {
        ErrorMensage = errorMensage;
    }
}
