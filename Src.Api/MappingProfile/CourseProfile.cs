using Mapster;
using Src.Application.Features.Course;
using Src.Application.Features.Course.Commands;
using Src.Application.Features.Course.Queries;
using Src.Domain.Course;
using Src.Domain.Dto;
using Src.Dto.Course;
using Src.Dto.Couse;

namespace Src.Api.MappingProfile;

public class CourseProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCourseRequestDto, CreateCourseRequest>();
        config.NewConfig<(string Username, CreateCourseRequestDto Request), CreateCourseCommand>()
            .Map(dest => dest.Request, src => src.Request)
            .Map(dest => dest.Username, src => src.Username);

        config.NewConfig<(string Username, int SubmissionId), GetCourseQuery>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.SubmissionId, src => src.SubmissionId);

        config.NewConfig<GetCourseResponse, GetCourseResponseDto>();

        config.NewConfig<GetDownloadedLessonRequestDto, GetDownloadedLessonRequest>();
        config.NewConfig<(string Username, GetDownloadedLessonRequest request), GetDownloadedLessonsQuery>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.request, src => src.request);

        config.NewConfig<GetDownloadedLessonsResponse, GetDownloadedLessonsResponseDto>();

        config.NewConfig<AddLessonMaterialRequestDto, AddLessonMaterialRequest>();
        config.NewConfig<(string Username, AddLessonMaterialRequest request), AddLessonMaterialQuery>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Request, src => src.request);

        config.NewConfig<string, GetUsersRegisteredCoursesQuery>()
            .Map(dest => dest.Username, src => src);

        config.NewConfig<(string Username, int SubmissionId),  RegisterUserCommand>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.SubmissionId, src => src.SubmissionId);

        config.NewConfig<(string Username, UpdateCourseRequest Request), UpdateCourseCommand>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.request, src => src.Username);
    }
}

