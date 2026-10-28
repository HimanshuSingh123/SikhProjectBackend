using Mapster;
using Src.Application.Features.Newsfeed.Commands;
using Src.Application.Features.Newsfeed.NewFolder;
using Src.Dto.NewsFeed;
using Src.Domain.NewsFeed;
using Src.Domain.Newsfeed;
using Src.Dto.Newsfeed;


namespace Src.Api.MappingProfile
{
    public class NewsfeedProfile : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<(string Username, int SubmissionId), GetNewsfeedQuery>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.SubmissionId, src => src.SubmissionId);

            config.NewConfig<GetNewsfeedResponse, GetNewsfeedResponseDto>();

            config.NewConfig<CreateNewsFeedRecordRequestDto, CreateNewsFeedRecordRequest>();
            config.NewConfig<(string Username, CreateNewsFeedRecordRequestDto Request), CreateNewsFeedCommand>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Request, src => src.Request);

            config.NewConfig<UpdateNewsFeedRequestDto, UpdateNewsFeedRequest>();
            config.NewConfig<(string Username, UpdateNewsFeedRequestDto Request), UpdateNewsfeedCommand>()
                .Map(dest => dest.Username, src => src.Username)
                .Map(dest => dest.Request, src => src.Request);
        }
    }
}
