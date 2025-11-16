using Mapster;
using Src.Domain.Item;
using Src.Dto.Item;

namespace Src.Api.MappingProfile;

public class ItemProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //ItemMetaData
        config.NewConfig<ItemMetaDataRequestDto, ItemMetaDataRequest>();
        config.NewConfig<ItemMetaDataResponse,  ItemMetaDataResponseDto>();

        //ViewItems
        config.NewConfig<ViewItemsRequestDto, ViewItemsRequest>();
        config.NewConfig<ViewItemsResponse, ViewItemsResponseDto>();

        //ViewImages
        config.NewConfig<ViewImageRequestDto, ViewImageRequest>();
        config.NewConfig<ViewImageResponse, ViewImageResponseDto>();
    }
}

