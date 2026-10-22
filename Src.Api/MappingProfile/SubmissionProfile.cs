using Mapster;
using Src.Application.Features.Submission.Commands;
using Src.Domain.Submission;
using Src.Dto.Submission;

namespace Src.Api.MappingProfile;

public class SubmissionProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateSubmissionRequestDto, CreateSubmissionRequest>();
        config.NewConfig<(string Username, CreateSubmissionRequestDto request), CreateSubmissionHelperCommand>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.Request, src => src.request);

        config.NewConfig<(string Username, int SubmissionId), DeleteSubmissionHelper>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.SubmissionId, src => src.SubmissionId);
    }
}

