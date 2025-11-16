namespace Src.Dto.Item;

public record ViewImageResponseDto
{
    public required byte[] Image { get; init; }
}

