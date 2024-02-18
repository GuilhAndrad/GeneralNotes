namespace GeneralNotes.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    Task<bool> ExistUserWithEmail(string email);
    Task<Entities.User?> RecoverByEmailPassword(string email, string password);
    Task<Entities.User?> RecoverByEmail(string email);
}
