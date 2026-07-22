using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Item;

namespace Src.Application.Features.ItemMetaData.Queries;

public class ItemMetaDataGetRequestQueryHandler : IRequestHandler<ItemMetaDataGetRequestQuery, ItemMetaDataResponse>
{
    private ILogger<ItemMetaDataGetRequestQueryHandler> _logger;
    private IItemRepository _itemRepository;
    public ItemMetaDataGetRequestQueryHandler(ILogger<ItemMetaDataGetRequestQueryHandler> logger, IItemRepository repository)
    {
        _logger = logger;
        _itemRepository = repository;
    }
    public async Task<ItemMetaDataResponse> Handle(ItemMetaDataGetRequestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Item meta data for item with {submissionId}", request.submissionId);
        
        var result = await _itemRepository.GetItemMetaDataAsync(request);

        _logger.LogInformation("Returned a result with Item Meta Data for {item}", result.Title);

        return result;
    }
}

