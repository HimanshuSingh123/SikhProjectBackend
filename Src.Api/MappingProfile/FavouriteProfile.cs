using Mapster;
using Src.Domain.Favourite;
using Src.Dto.Favourite;

namespace Src.Api.MappingProfile;

public class FavouriteProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<ViewFavouritesRequestDto, ViewFavouritesRequest>();
        config.NewConfig<ViewFavouritesResponse,  ViewFavouritesResponseDto>();
    }
}

