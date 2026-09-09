using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.MerchItem.Commands;

public class UploadMerchItemImageCommandHandler : IRequestHandler<UploadMerchItemImageCommand, bool>
{
    private readonly ILogger<UploadMerchItemImageCommandHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public UploadMerchItemImageCommandHandler(ILogger<UploadMerchItemImageCommandHandler> logger, IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(UploadMerchItemImageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Uploading image for merch item with submission ID {SubmissionId} for user {User}", request.Request.SubmissionId, request.User);

        var response = await _repository.UploadMerchItemImage(request.Request, cancellationToken);

        if (response)
        {
            _logger.LogInformation("Successfully uploaded image for merch item with submission ID {SubmissionId} for user {User}", request.Request.SubmissionId, request.User);
        }
        else
        {
            _logger.LogWarning("Failed to upload image for merch item with submission ID {SubmissionId} for user {User}", request.Request.SubmissionId, request.User);
        }

        return response;
    }
}