using Microsoft.EntityFrameworkCore;
using Src.Application.Interfaces;
using Src.Domain.Authentication;
using Src.Domain.Entities;
using Src.Infrastructure.Persistance;

namespace Src.Infrastructure.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthRepository(ApplicationDbContext dbContext)
    {
        this._dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> CheckIfExistingEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.User.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> CheckIfExistingUsername(string username, CancellationToken cancellationToken)
    {
        return await _dbContext.User.AnyAsync((u => u.Username == username), cancellationToken);
    }

    public async Task<User?> FetchUser(string username)
    {
        var user = await _dbContext.User.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        return user;
    }

    public async Task<int> CreateUser(User user, CancellationToken cancellationToken)
    {
        user.CreatedAt = DateTime.UtcNow;
        _dbContext.User.Add(user);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.UserId;
    }
}

