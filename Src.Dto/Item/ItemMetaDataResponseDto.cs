using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Dto.Item;

public record ItemMetaDataResponseDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
    public required byte[] Image { get; init; }
    public required string Size { get; init; }
    public required int QuantityMax { get; init; }
    public required int QuantityMin { get; init; }
    
    public required decimal Price { get; init; }
    public required decimal Rating { get; init; }
}

