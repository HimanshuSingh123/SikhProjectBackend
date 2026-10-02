using MediatR;
using Src.Domain.Course;

namespace Src.Application.Features.Course;

public record CreateCourseCommand(string Username, CreateCourseRequest Request) : IRequest<bool>;

