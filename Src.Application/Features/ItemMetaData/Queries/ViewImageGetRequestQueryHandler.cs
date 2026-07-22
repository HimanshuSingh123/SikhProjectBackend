using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;
using Src.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.ItemMetaData.Queries;

public class ViewImageGetRequestQueryHandler : IRequestHandler<ViewImageGetRequestQuery, IEnumerable<ViewImageResponse>>
{
    private readonly ILogger<ViewImageGetRequestQueryHandler> _logger;
    private IItemRepository _itemRepository;

    public ViewImageGetRequestQueryHandler(ILogger<ViewImageGetRequestQueryHandler> logger, IItemRepository itemRepository)
    {
        _logger = logger;
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<ViewImageResponse>> Handle(ViewImageGetRequestQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Getting Item meta data for item with {submissionId}", request.SubmissionId);

        var result = await _itemRepository.GetViewImagesAsync(request);

        _logger.LogInformation("Got {result} pictures from the request", result.Count());

        return result;

    }
}

