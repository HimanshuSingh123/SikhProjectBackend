using Mapster;
using Src.Application.Features.Favourite.Commands;
using Src.Application.Features.Favourite.Queries;
using Src.Domain.Favourite;
using Src.Dto.Favourite;

namespace Src.Api.MappingProfile;

public class FavouriteProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ViewFavouritesRequestDto, ViewFavouriteGetRequestQuery>();
        config.NewConfig<ViewFavouritesResponse,  ViewFavouritesResponseDto>();

        config.NewConfig<(string Username, int submissionId), AddToFavouritesCommand>()
            .Map(dest => dest.Username, src => src.Username)
            .Map(dest => dest.submissionId, src => src.submissionId);

        config.NewConfig<(string Username, int Fav_Id), DeleteFromFavouritesCommand>()
            .Map(dest => dest.Fav_Id, src => src.Fav_Id)
            .Map(dest => dest.Username, src => src.Username);
    }
}

