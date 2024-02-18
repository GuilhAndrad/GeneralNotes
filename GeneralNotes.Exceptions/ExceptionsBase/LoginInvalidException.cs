namespace GeneralNotes.Exceptions.ExceptionsBase;
public class LoginInvalidException : GeneralNotesException
{
    public LoginInvalidException() : base(ResourceErrorMensages.LOGIN_INVALIDO)
    {

    }
}
