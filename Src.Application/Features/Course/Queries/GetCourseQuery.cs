using MediatR;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Queries;

public record GetCourseQuery(string Username, int SubmissionId) : IRequest<GetCourseResponse?>;


