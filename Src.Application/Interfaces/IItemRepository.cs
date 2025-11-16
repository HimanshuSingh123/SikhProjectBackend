using Src.Domain.Item;

namespace Src.Application.Interfaces;

public interface IItemRepository
{
        Task<ItemMetaDataResponse> GetItemMetaDataAsync(ItemMetaDataRequest request);
        Task<IEnumerable<ViewItemsResponse>> GetViewItemsAsync(ViewItemsRequest request);

        Task<IEnumerable<ViewImageResponse>> GetViewImagesAsync(ViewImageRequest request);
}

