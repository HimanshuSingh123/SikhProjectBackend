using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Item;

namespace Src.Application.Features.ItemMetaData.Queries;

public class ViewItemsGetRequestQueryHandler : IRequestHandler<ViewItemsGetRequestQuery, IEnumerable<ViewItemsResponse>>
{
    private readonly ILogger<ViewItemsGetRequestQueryHandler> _logger;
    private IItemRepository _itemRepository;

    public ViewItemsGetRequestQueryHandler(ILogger<ViewItemsGetRequestQueryHandler> logger, IItemRepository itemRepository)
    {
        _logger = logger;
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<ViewItemsResponse>> Handle(ViewItemsGetRequestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Getting items with filters: " +
            "Search={SearchData}, PageSize={PageSize}, Page={Pages}, " +
            "PriceMax={PriceMax}, PriceMin={PriceMin}, SizeMin={SizeMin}, SizeMax={SizeMax}, " +
            "Category={Category}, Asc={Ascending}, Desc={Descending}",
            request.searchData,
            request.pageSize,
            request.pages,
            request.priceMax,
            request.priceMin,
            request.sizeMin,
            request.sizeMax,
            request.category,
            request.ascending,
            request.descending
        );


        var result = await _itemRepository.GetViewItemsAsync(request);

        _logger.LogInformation("Returned a {count} results", result);

        return result;
    }


}

