using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Item;

public record ItemMetaDataResponse
{
    public required String Title { get; init; }
    public required String Description { get; init; }
    public required byte[] Image { get; init; }
    public required String Size { get; init; }
    public required int QuantityMax { get; init; }
    public required int QuantityMin { get; init; }
    
    public required decimal Price { get; init; }
    public required decimal Rating { get; init; }
}

