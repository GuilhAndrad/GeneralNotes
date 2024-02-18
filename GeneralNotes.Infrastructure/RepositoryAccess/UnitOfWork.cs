using GeneralNotes.Domain.Repositories;

namespace GeneralNotes.Infrastructure.RepositoryAccess;
public sealed class UnitOfWork(GeneralNotesContext context) : IDisposable, IUnitOfWork
{
    private readonly GeneralNotesContext _context = context;
    private bool _disposed;

    public async Task Commit()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
        }
        _disposed = true;
    }
}