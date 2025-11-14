using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Interfaces.Common;

public interface ICurrentUser
{
    String UserName { get; }
    List<String> AccountType { get; }
    String? Email { get; }
    int? UserId { get; }
}

