using MediatR;
using Src.Domain.Course;

namespace Src.Application.Features.Course.Queries;

public record AddLessonMaterialQuery(string Username, AddLessonMaterialRequest Request) : IRequest<bool>;


