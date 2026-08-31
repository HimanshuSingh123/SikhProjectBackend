using Mapster;
using Src.Application.Features.MerchItem.Commands;
using Src.Application.Features.MerchItem.NewFolder;
using Src.Domain.MerchItems;
using Src.Dto.MerchItems;

namespace Src.Api.MappingProfile;

public class MerchItemProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SaveMerchItemRequestDto, SaveMerchItemRequest>();
        config.NewConfig<(string User, SaveMerchItemRequestDto Request), SaveMerchItemCommand>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest.Request, src => src.Request);

        config.NewConfig<CreateMerchItemRequestDto, CreateMerchItemRequest>();
        config.NewConfig<(string User, CreateMerchItemRequestDto request), CreateMerchItemCommand>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest.Request, src => src.request);

        config.NewConfig<(string User, int submissionId), GetMerchItemQuery>()
            .Map(dest => dest.User, src => src.User)
            .Map(dest => dest.SubmissionId, src => src.submissionId);
    }
}

