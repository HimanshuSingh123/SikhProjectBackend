namespace Src.Domain.Entities;

public class Temples
{
    public int TempleId { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public float lat { get; set; } = default!;
    public float lon { get; set; } = default!;
}

