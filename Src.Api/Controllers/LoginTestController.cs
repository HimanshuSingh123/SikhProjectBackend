using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Src.Api.Controllers;

[ApiController]
[Route("LoginTest")]
public class LoginTestController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public LoginTestController(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }


    [HttpPost("GetToken")]
    public IActionResult GetToken()
    {


        //        public String UserName => _principal?.FindFirst("username")?.Value!;
        //public String? Email => _principal?.FindFirst("email")?.Value;
        //public int? UserId => int.TryParse(_principal?.FindFirst("userid")?.Value, out var id) ? id : null;

        //public List<String> AccountType => _principal?.FindAll(c => c.Type == ClaimTypes.Role)
        //                                              .Select(c => c.Value)
        //                                              .ToList() ?? new List<string>();

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Api:Key"]!));
        var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim("username", "himanshu"),
            new Claim("email", "himanshu@himanshu12.com"),
            new Claim("userId", "1"),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var newJwtSecruityToken = new JwtSecurityToken
            (
               issuer: _configuration["Api:issuer"],
               audience: _configuration["Api:audience"],
               signingCredentials: signingCredentials,
               claims: claims,
               expires: DateTime.UtcNow.AddHours(1)
            );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(newJwtSecruityToken);

        return Ok(tokenString);


    }
}

