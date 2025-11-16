using Src.Application.Interfaces;
using Src.Domain.Item;

namespace Src.Infrastructure.Repository;

public class ItemRepository : IItemRepository
{
    public async Task<ItemMetaDataResponse> GetItemMetaDataAsync(ItemMetaDataRequest request)
    {
        var response =  new ItemMetaDataResponse()
        {
            Title = string.Empty,   // TODO: map from DB / request
            Description = string.Empty,
            Image = [],
            Size = string.Empty,              // or string.Empty / whatever the type is
            QuantityMax = 0,
            QuantityMin = 0,
            Price = 0m,
            Rating = 0
        };

        return await Task.FromResult(response);
    }

    public async Task<IEnumerable<ViewImageResponse>> GetViewImagesAsync(ViewImageRequest request)
    {
        var response = new List<ViewImageResponse>();
        return await Task.FromResult(response);
    }

    public async Task<IEnumerable<ViewItemsResponse>> GetViewItemsAsync(ViewItemsRequest request)
    {
        var response = new List<ViewItemsResponse>();
        return await Task.FromResult(response);
    }
}

