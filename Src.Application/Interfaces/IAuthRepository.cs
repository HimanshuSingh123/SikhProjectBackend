using Src.Domain.Authentication;
using Src.Domain.Entities;

namespace Src.Application.Interfaces;

public interface IAuthRepository
{
    public Task<User?> FetchUser(string Username);
    public Task<bool> CheckIfExistingUsername(string Username, CancellationToken cancellationToken);
    public Task<bool> CheckIfExistingEmail(string Email, CancellationToken cancellationToken);
    public Task<int> CreateUser(User request, CancellationToken cancellationToken);
}

