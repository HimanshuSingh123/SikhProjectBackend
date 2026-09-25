using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Queries;

public class GetUsersRegisteredCoursesQueryHandler: IRequestHandler<GetUsersRegisteredCoursesQuery, IReadOnlyList<GetCourseResponse>>
{
    private readonly ICourseRepository _repository;
    private readonly ILogger<GetUsersRegisteredCoursesQueryHandler> _logger;

    public GetUsersRegisteredCoursesQueryHandler(
        ILogger<GetUsersRegisteredCoursesQueryHandler> logger,
        ICourseRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IReadOnlyList<GetCourseResponse>> Handle(
        GetUsersRegisteredCoursesQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Retrieving registered courses for user {Username}",
            request.Username);

        var result = await _repository.GetUsersRegisteredCourses(
            request.Username, cancellationToken);

        var courses = result.ToList();

        if (courses.Count > 0)
        {
            _logger.LogInformation(
                "Successfully retrieved {CourseCount} registered courses for user {Username}",
                courses.Count, request.Username);
        }
        else
        {
            _logger.LogInformation(
                "No registered courses found for user {Username}",
                request.Username);
        }

        return courses;
    }
}