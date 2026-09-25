using MediatR;
using Src.Domain.Course;
using Src.Dto.Course;

namespace Src.Application.Features.Course.Queries;

public record GetDownloadedLessonsQuery(string Username, GetDownloadedLessonRequest request) : IRequest<GetDownloadedLessonsResponse?>;


