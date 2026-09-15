using MediatR;
using Src.Domain.Couse;

namespace Src.Application.Features.Course;

public record CreateCourseCommand(string Username, CreateCourseRequest Request) : IRequest<bool>;

