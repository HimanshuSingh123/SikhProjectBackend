using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace Src.Application.Features.Authentication;

/// <summary>
/// Handles authentication login requests by validating user credentials and generating a JSON Web Token.
/// </summary>
public class AuthLoginCommandHandler : IRequestHandler<AuthLoginCommand, string>
{
    private readonly IAuthRepository _repo;
    private readonly ILogger<AuthLoginCommandHandler> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthLoginCommandHandler"/> class.
    /// </summary>
    /// <param name="repo">The repository used to retrieve user authentication data.</param>
    /// <param name="logger">The logger used to record authentication activity.</param>
    /// <param name="passwordHasher">The password hasher used to verify user passwords.</param>
    /// <param name="configuration">The application configuration containing JWT settings.</param>
    /// <exception cref="ArgumentNullException">Thrown when one of the required dependencies is null.</exception>
    public AuthLoginCommandHandler(IAuthRepository repo, ILogger<AuthLoginCommandHandler> logger, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    /// <summary>
    /// Validates the provided login credentials and generates a signed JSON Web Token for the authenticated user.
    /// </summary>
    /// <param name="request">The login request containing the username and password.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel the operation.</param>
    /// <returns>A task containing the generated JSON Web Token.</returns>
    /// <exception cref="InvalidCredentialException">Thrown when the username or password is incorrect.</exception>
    public async Task<string> Handle(AuthLoginCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Login attempt received for username {Username}", request.Username);

        var user = await _repo.FetchUser(request.Username);

        if (user == null)
        {
            _logger.LogWarning("Login failed because username {Username} was not found", request.Username);

            throw new InvalidCredentialException("Wrong username or password");
        }

        var verificationResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.HashedPass,
            request.Password
            );

        if (verificationResult == PasswordVerificationResult.Failed)
        {
            _logger.LogWarning("Login failed because password verification failed for username {Username}", request.Username);

            throw new InvalidCredentialException("Wrong username or password");
        }

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Api:Key"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("username", user.Username),
            new Claim("email", user.Email),
            new Claim("userId", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, user.AccountTypeName)
        };

        var newJwtSecruityToken = new JwtSecurityToken
            (
               issuer: _configuration["Api:issuer"],
               audience: _configuration["Api:audience"],
               signingCredentials: signingCredentials,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(24)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(newJwtSecruityToken);

        _logger.LogInformation("Login succeeded for username {Username} with role {Role}", user.Username, user.AccountTypeName);

        return tokenString;
    }
}