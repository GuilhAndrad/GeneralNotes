namespace GeneralNotes.Application.Services.UserLogged;
public interface ILoggedUser
{
    Task<Domain.Entities.User> RecoverUser();
}
