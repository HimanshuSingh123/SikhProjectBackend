using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Dto.Common;

public record UserContextDto
{
    public required String UserName {  get; init; }
    public required String AccountType {  get; init; }

    public String? Email {  get; init; }
    public int? UserId { get; init; }
}

