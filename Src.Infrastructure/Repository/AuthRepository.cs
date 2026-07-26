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

    public async Task<bool> CheckIfExistingEmail(string email)
    {
        return await _dbContext.User.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> CheckIfExistingUsername(string username)
    {
        return await _dbContext.User.AnyAsync(u => u.Username == username);
    }

    public async Task<User?> FetchUser(string username)
    {
        var user = await _dbContext.User.AsNoTracking().SingleOrDefaultAsync(u => u.Username == username);

        return user;
    }
}

