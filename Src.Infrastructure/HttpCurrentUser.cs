using Microsoft.AspNetCore.Http;
using Src.Application.Interfaces.Common;
using System.Security.Claims;
namespace Src.Infrastructure;

/*basically the purpose of this class was to make it so that I don't need to pass usercontext around through the layers. I can just 
 * instantiate it through dependency injection in the layers i need via their controllers
 */
public class HttpCurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor? _contextAccessor;
    private ClaimsPrincipal? _principal => _contextAccessor?.HttpContext?.User;

    public HttpCurrentUser(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public String UserName => _principal?.FindFirst("username")?.Value!;
    public String? Email => _principal?.FindFirst("email")?.Value;
    public int? UserId => int.TryParse(_principal?.FindFirst("userId")?.Value, out var id) ? id : null;

    public List<String> AccountType => _principal?.FindAll(c => c.Type == ClaimTypes.Role)
                                                  .Select(c => c.Value)
                                                  .ToList() ?? new List<string>();
                   
}

