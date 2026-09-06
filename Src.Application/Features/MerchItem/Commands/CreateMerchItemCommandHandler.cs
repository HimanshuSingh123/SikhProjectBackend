using MediatR;
using Microsoft.Extensions.Logging;
using Src.Application.Interfaces;

namespace Src.Application.Features.MerchItem.Commands;

public class CreateMerchItemCommandHandler : IRequestHandler<CreateMerchItemCommand, bool>
{
    private readonly ILogger<CreateMerchItemCommandHandler> _logger;
    private readonly IMerchItemRepository _repository;

    public CreateMerchItemCommandHandler(
        ILogger<CreateMerchItemCommandHandler> logger,
        IMerchItemRepository repository)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<bool> Handle(
        CreateMerchItemCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "User {User} is creating merch item with submission ID {SubmissionId}",
            request.User,
            request.Request.SubmissionId);

        var result = await _repository.CreateMerchItem(request.Request, cancellationToken);

        if (result)
        {
            _logger.LogInformation(
                "User {User} successfully created merch item with submission ID {SubmissionId}",
                request.User,
                request.Request.SubmissionId);
        }
        else
        {
            _logger.LogWarning(
                "User {User} failed to create merch item with submission ID {SubmissionId}",
                request.User,
                request.Request.SubmissionId);
        }

        return result;
    }
}