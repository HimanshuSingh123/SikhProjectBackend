using MediatR;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Commands;

public record UpdateCourseCommand(string Username, UpdateCourseRequest request) : IRequest<bool>;

