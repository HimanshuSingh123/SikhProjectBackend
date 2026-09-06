using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.MerchItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.MerchItem.Queries;

public class SearchMerchItemQueryHandler : IRequestHandler<SearchMerchItemQuery, SearchMerchItemResponse?>
{
    private readonly ILogger<SearchMerchItemQueryHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public SearchMerchItemQueryHandler(ILogger<SearchMerchItemQueryHandler> logger, IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<SearchMerchItemResponse?> Handle(SearchMerchItemQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Searching merch items for user ID {User} with title {Title}, page {Page}, page size {PageSize}, and size {Size}",
            request.User,
            request.Request.Title,
            request.Request.Page,
            request.Request.PageSize,
            request.Request.Size);

        var response = await _repository.SearchMerchItem(request.Request, cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("No merch items were found for user ID {User} with title {Title}, page {Page}, page size {PageSize}, and size {Size}",
                request.User,
                request.Request.Title,
                request.Request.Page,
                request.Request.PageSize,
                request.Request.Size);

            return null;
        }
        else
        {
            _logger.LogInformation("Successfully retrieved merch items for user ID {User} with title {Title}, page {Page}, page size {PageSize}, and size {Size}",
                request.User,
                request.Request.Title,
                request.Request.Page,
                request.Request.PageSize,
                request.Request.Size);
        }

        return response;
    }
}