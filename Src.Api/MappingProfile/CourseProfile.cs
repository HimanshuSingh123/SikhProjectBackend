using Mapster;
using Src.Application.Features.Course;
using Src.Application.Features.Course.Queries;
using Src.Domain.Course;
using Src.Domain.Couse;
using Src.Dto.Course;

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

    }
}

