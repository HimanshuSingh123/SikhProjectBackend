using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Authentication.Commands;
using Src.Dto.Authentication;

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

        [HttpPost("Register")]
        public async Task<ActionResult<string>> RegisterRetrieveToken(RegisterRequestDto request)
        {
            var command = _mapper.Map<AuthRegisterCommand>(request);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}
