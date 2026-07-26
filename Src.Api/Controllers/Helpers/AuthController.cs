using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Src.Application.Features.Authentication;
using Src.Dto.Authentication;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Src.Api.AuthController.Helpers
{
    [ApiController]
    [Route("Authentication")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AuthController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost("Login")]
        public async Task<ActionResult<string>> LoginRetrieveToken(LoginRequestDto request)
        {
            var command = _mapper.Map<AuthLoginCommand>(request);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
