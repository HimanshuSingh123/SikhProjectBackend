using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Features.MerchItem.NewFolder;
using Src.Application.Interfaces;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.Queries;

public class GetMerchItemQueryHandler
    : IRequestHandler<GetMerchItemQuery, GetMerchItemResponse?>
{
    private readonly ILogger<GetMerchItemQueryHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public GetMerchItemQueryHandler(
        ILogger<GetMerchItemQueryHandler> logger,
        IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<GetMerchItemResponse?> Handle(
        GetMerchItemQuery request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Retrieving merch item with submission ID {SubmissionId}",
            request.SubmissionId);

        var response = await _repository.GetMerchItem(request.SubmissionId);

        if (response == null)
        {
            _logger.LogWarning(
                "Merch item with submission ID {SubmissionId} was not found",
                request.SubmissionId);
        }
        else
        {
            _logger.LogInformation(
                "Successfully retrieved merch item with submission ID {SubmissionId}",
                request.SubmissionId);
        }

        return response;
    }
}