using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Item;

namespace Src.Application.Features.Query.ItemMetaData;

public class ItemMetaDataGetRequestHandler : IRequestHandler<ItemMetaDataGetRequest, ItemMetaDataResponse>
{
    private ILogger<ItemMetaDataGetRequestHandler> _logger;
    private IItemRepository _itemRepository;
    public ItemMetaDataGetRequestHandler(ILogger<ItemMetaDataGetRequestHandler> logger, IItemRepository repository)
    {
        _logger = logger;
        _itemRepository = repository;
    }
    public async Task<ItemMetaDataResponse> Handle(ItemMetaDataGetRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Item meta data for item with {submissionId}", request.submissionId);
        
        var result = await _itemRepository.GetItemMetaDataAsync(request);

        _logger.LogInformation("Returned a result with Item Meta Data for {item}", result.Title);

        return result;
    }
}

