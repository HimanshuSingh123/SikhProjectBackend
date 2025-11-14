using Src.Application.Interfaces;
using Src.Domain.Item;

namespace Src.Infrastructure.Repository;

public class ItemRepository : IItemRepository
{
    public Task<ItemMetaDataResponse> GetItemMetaDataAsync(ItemMetaDataRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ViewItemsResponse>> GetViewItemsAsync(ViewItemsRequest request)
    {
        throw new NotImplementedException();
    }
}

