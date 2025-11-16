using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Domain.Item;

public record ViewImageResponse
{
    public required byte[] Image { get; init; }
}

