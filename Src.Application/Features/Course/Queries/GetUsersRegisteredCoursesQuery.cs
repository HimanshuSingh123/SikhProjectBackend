using MediatR;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Queries;

public record GetUsersRegisteredCoursesQuery(string Username) : IRequest<IReadOnlyList<GetCourseResponse>>;
    
    

