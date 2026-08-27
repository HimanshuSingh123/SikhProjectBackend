namespace Src.Dto.Authentication;

public record RegisterRequestDto
{
    public required string Email { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
}

