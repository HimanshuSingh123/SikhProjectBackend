using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.MerchItem.Commands;

public class SaveMerchItemCommandHandler : IRequestHandler<SaveMerchItemCommand, bool>
{
    private readonly ILogger<SaveMerchItemCommandHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public SaveMerchItemCommandHandler(
        ILogger<SaveMerchItemCommandHandler> logger,
        IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(
        SaveMerchItemCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Saving changes for merch item with submission ID {SubmissionId} for user {User}",
            request.Request.SubmissionId,
            request.User);

        var response = await _repository.SaveMerchItemChanges(request.Request);

        if (response)
        {
            _logger.LogInformation(
                "Successfully saved changes for merch item with submission ID {SubmissionId} for user {User}",
                request.Request.SubmissionId,
                request.User);
        }
        else
        {
            _logger.LogWarning(
                "Failed to save changes for merch item with submission ID {SubmissionId} for user {User}",
                request.Request.SubmissionId,
                request.User);
        }

        return response;
    }
}