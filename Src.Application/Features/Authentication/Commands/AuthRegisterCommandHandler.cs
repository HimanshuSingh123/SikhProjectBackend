using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Src.Application.Interfaces;
using Src.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;

namespace Src.Application.Features.Authentication.Commands;

public class AuthRegisterCommandHandler : IRequestHandler<AuthRegisterCommand, string>
{
    private readonly IAuthRepository _repo;
    private readonly ILogger<AuthRegisterCommandHandler> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthRegisterCommandHandler"/> class.
    /// </summary>
    /// <param name="repo">The repository used to retrieve user authentication data.</param>
    /// <param name="logger">The logger used to record authentication activity.</param>
    /// <param name="passwordHasher">The password hasher used to verify user passwords.</param>
    /// <param name="configuration">The application configuration containing JWT settings.</param>
    /// <exception cref="ArgumentNullException">Thrown when one of the required dependencies is null.</exception>
    public AuthRegisterCommandHandler(IAuthRepository repo, ILogger<AuthRegisterCommandHandler> logger, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<string> Handle(AuthRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registration attempt received for username {Username}", request.Username);

        var emailCheck = await _repo.CheckIfExistingEmail(request.Email, cancellationToken);
        var usernameCheck = await _repo.CheckIfExistingUsername(request.Username, cancellationToken);

        if (emailCheck || usernameCheck)
        {
            _logger.LogWarning("Registration failed for username {Username}. EmailExists: {EmailExists}, UsernameExists: {UsernameExists}", request.Username, emailCheck, usernameCheck);

            throw new InvalidCredentialException("Username or Email already exists.");
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            AccountTypeName = "User"
        };

        var hashedPassword = _passwordHasher.HashPassword(user, request.Password);

        user.HashedPass = hashedPassword;

        var userId = await _repo.CreateUser(user, cancellationToken);

        user.UserId = userId;

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Api:Key"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("username", user.Username),
            new Claim("email", user.Email),
            new Claim("userId", user.UserId.ToString()),
            new Claim(ClaimTypes.Role, "User")
        };

        var newJwtSecruityToken = new JwtSecurityToken
            (
               issuer: _configuration["Api:issuer"],
               audience: _configuration["Api:audience"],
               signingCredentials: signingCredentials,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(1)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(newJwtSecruityToken);

        _logger.LogInformation("Registration succeeded for username {Username} with user ID {UserId}", user.Username, user.UserId);

        return tokenString;
    }
}