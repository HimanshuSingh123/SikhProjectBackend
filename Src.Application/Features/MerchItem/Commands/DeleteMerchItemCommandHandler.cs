using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.MerchItem.Commands;

public class DeleteMerchItemCommandHandler : IRequestHandler<DeleteMerchItemCommand, bool>
{
    private readonly ILogger<DeleteMerchItemCommandHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public DeleteMerchItemCommandHandler(ILogger<DeleteMerchItemCommandHandler> logger, IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(DeleteMerchItemCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("User ID {UserId} is deleting merch item with submission ID {SubmissionId}",
            request.UserId,
            request.SubmissionId);

        var deleted = await _repository.DeleteMerchItem(request.SubmissionId, cancellationToken);

        if (!deleted)
        {
            _logger.LogWarning("Merch item with submission ID {SubmissionId} could not be deleted for user ID {UserId}",
                request.SubmissionId,
                request.UserId);

            return false;
        }

        _logger.LogInformation("Successfully deleted merch item with submission ID {SubmissionId} for user ID {UserId}",
            request.SubmissionId,
            request.UserId);

        return true;
    }
}