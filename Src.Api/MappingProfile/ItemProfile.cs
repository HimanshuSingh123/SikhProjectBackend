using Mapster;
using Src.Application.Features.Query.ItemMetaData;
using Src.Domain.Item;
using Src.Dto.Item;

namespace Src.Api.MappingProfile;

public class ItemProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //ItemMetaData
        config.NewConfig<ItemMetaDataRequestDto, ItemMetaDataGetRequestQuery>();
        config.NewConfig<ItemMetaDataResponse,  ItemMetaDataResponseDto>();

        //ViewItems
        config.NewConfig<ViewItemsRequestDto, ViewItemsGetRequestQuery>();
        config.NewConfig<ViewItemsResponse, ViewItemsResponseDto>();

        //ViewImages
        config.NewConfig<ViewImageRequestDto, ViewImageGetRequestQuery>();
        config.NewConfig<ViewImageResponse, ViewImageResponseDto>();
    }
}

