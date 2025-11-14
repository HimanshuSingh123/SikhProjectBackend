using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Dto.Item;

public record ItemMetaDataRequestDto
{
    public required int submissionId { get; init; }
}

