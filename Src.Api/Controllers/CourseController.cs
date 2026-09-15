using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Course.Queries;
using Src.Application.Features.Favourite.Commands;
using Src.Domain.Course;
using Src.Domain.Entities;
using Src.Dto.Course;
using Src.Infrastructure;

namespace Src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly HttpCurrentUser _currentUser;

    public CourseController(IMediator mediator, IMapper mapper, HttpCurrentUser currentUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    [HttpGet("GetCourse/{SubmissionId}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<GetCourseResponseDto?>> GetCourse(int submissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetCourseQuery>((_currentUser.UserName, submissionId));
        var result = await _mediator.Send(query, cancellationToken);
        return result != null ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }


    [HttpPost("CreateCourse")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin")]
    public async Task<ActionResult<bool>> CreateCourse(int submissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<AddToFavouritesCommand>((_currentUser.UserName, submissionId));
        var result = await _mediator.Send(query, cancellationToken);
        return result ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }
}

