using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Course.Queries;
using Src.Application.Features.Newsfeed.NewFolder;
using Src.Application.Interfaces.Common;
using Src.Dto.Newsfeed;
using Src.Domain.Newsfeed;
using Src.Dto.NewsFeed;
using Src.Application.Features.Newsfeed.Commands;

namespace Src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsfeedController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public NewsfeedController(IMediator mediator, IMapper mapper, ICurrentUser currentUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    [HttpGet]
    [Route("{SubmissionId}")]
    public async Task<ActionResult<GetNewsfeedResponseDto>> GetNewsFeedBySubmissionId([FromRoute] int SubmissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetNewsfeedQuery>((_currentUser.UserName, SubmissionId));
        var response = await _mediator.Send(query, cancellationToken);

        return response != null ? Ok(response) : NotFound();
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<bool>> CreateNewsFeed([FromForm] CreateNewsFeedRecordRequestDto Request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<CreateNewsFeedCommand>((_currentUser.UserName, Request));
        var response = await _mediator.Send(query, cancellationToken);

        return response != false ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpPost]
    [Route("Update")]
    public async Task<ActionResult<bool>> UpdateNewsfeed([FromForm] UpdateNewsFeedRequestDto Request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<UpdateNewsfeedCommand>((_currentUser.UserName, Request));
        var response = await _mediator.Send(query, cancellationToken);

        return response != false ? Ok(response) : StatusCode(StatusCodes.Status500InternalServerError);
    }

}

