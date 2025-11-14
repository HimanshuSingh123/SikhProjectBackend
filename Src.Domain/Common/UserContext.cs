namespace Src.Domain.Common;

public record UserContext
{
    public required String UserName { get; init; }
    public required String AccountType { get; init; }

    public String? Email { get; init; }
    public int? UserId { get; init; }
}

