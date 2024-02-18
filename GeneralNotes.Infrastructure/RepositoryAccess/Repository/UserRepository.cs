using GeneralNotes.Domain.Entities;
using GeneralNotes.Domain.Repositories.User;
using Microsoft.EntityFrameworkCore;

namespace GeneralNotes.Infrastructure.RepositoryAccess.Repository;
public class UserRepository(GeneralNotesContext context) : IUserReadOnlyRepository, IUserUpdateOnlyRepository, IUserWriteOnlyRepository
{
    private readonly GeneralNotesContext _context = context;

    public async Task Add(User user)
    {
        await _context.Users
            .AddAsync(user);
    }

    public async Task<bool> ExistUserWithEmail(string email)
    {
        return await _context.Users
            .AnyAsync(x => x.Email.Equals(email));
    }

    public async Task<User?> GetById(long userId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<User?> RecoverByEmail(string email)
    {
        return await _context.Users
           .AsNoTracking()
           .FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User?> RecoverByEmailPassword(string email, string password)
    {
        return await _context.Users.AsNoTracking()
           .FirstOrDefaultAsync(x => x.Email
           .Equals(email) && x.Password
           .Equals(password));
    }

    public void Update(User user)
    {
        _context.Users
            .Update(user);
    }
}
