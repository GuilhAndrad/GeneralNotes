namespace GeneralNotes.Domain.Repositories;
public interface IUnitOfWork
{
    Task Commit();
}
