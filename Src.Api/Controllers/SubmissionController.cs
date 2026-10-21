using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Submission.Commands;
using Src.Application.Interfaces.Common;
using Src.Dto.Submission;

namespace Src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubmissionController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ICurrentUser _httpCurrentUser;
    public SubmissionController(IMapper mapper, IMediator mediator, ICurrentUser currentUser)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _httpCurrentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    [HttpPost("CreateSubmission")]
    [Authorize(Policy = "CanCreateSubmission")]
    public async Task<ActionResult<int>> CreateSubmissionHelper([FromQuery] CreateSubmissionRequestDto request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateSubmissionHelperCommand>((_httpCurrentUser.UserName, request));
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(response);
    }

}

