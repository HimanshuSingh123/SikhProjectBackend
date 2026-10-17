using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Src.Application.Features.Course;
using Src.Application.Features.Course.Commands;
using Src.Application.Features.Course.Queries;
using Src.Application.Interfaces.Common;
using Src.Domain.Course;
using Src.Domain.Dto;
using Src.Domain.Entities;
using Src.Dto.Course;
using Src.Dto.Couse;
using Src.Infrastructure;

namespace Src.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public CourseController(IMediator mediator, IMapper mapper, ICurrentUser currentUser)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _currentUser = currentUser ?? throw new ArgumentNullException(nameof(currentUser));
    }

    [HttpGet("GetCourse/{SubmissionId}")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<GetCourseResponseDto?>> GetCourse([FromRoute] int submissionId, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetCourseQuery>((_currentUser.UserName, submissionId));
        var result = await _mediator.Send(query, cancellationToken);
        return result != null ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }


    [HttpPost("CreateCourse")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin")]
    public async Task<ActionResult<bool>> CreateCourse([FromForm] CreateCourseRequestDto request, CancellationToken cancellationToken)
    {

        var query = _mapper.Map<CreateCourseCommand>((_currentUser.UserName, request));
        var result = await _mediator.Send(query, cancellationToken);
        return result ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpGet("DownloadLessonMaterial")]
    [Authorize(Roles = "User,Admin")]
    public async Task<ActionResult<GetDownloadedLessonsResponseDto?>> GetDownloadableLessons([FromQuery] GetDownloadedLessonRequestDto request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetDownloadedLessonsQuery>((_currentUser.UserName, request));
        var result = await _mediator.Send(query);
        return result != null ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpPost("UploadLessonMaterial")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin")]
    public async Task<ActionResult<bool>> UploadLessonMaterial([FromForm] AddLessonMaterialRequestDto Request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<AddLessonMaterialQuery>((_currentUser.UserName, Request));
        var result = await _mediator.Send(query, cancellationToken);
        return result ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }

    [HttpGet("GetUserCourses/{Username}")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin,User")]
    public async Task<ActionResult<IReadOnlyList<GetCourseResponse>>> GetUserCourses([FromRoute] string Username,  CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetUsersRegisteredCoursesQuery>(_currentUser.UserName);
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(result);
    }

    [HttpPost("RegisterUser/{submissionId}")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin,User")]
    public async Task<ActionResult<bool>> RegisterUser([FromRoute] int submissionId, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(_currentUser.UserName, submissionId);
        var result = await _mediator.Send(command, cancellationToken);
        return result ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }


    [HttpPut("UpdateCourse")]
    [Authorize(Roles = "Instructor,Admin,SysAdmin")]
    public async Task<ActionResult<bool>> UpdateCourse([FromForm] UpdateCourseRequestDto request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateCourseCommand>((_currentUser.UserName, request));
        var result = await _mediator.Send(command, cancellationToken);

        return result ? Ok(result) : StatusCode(StatusCodes.Status404NotFound);
    }

}

