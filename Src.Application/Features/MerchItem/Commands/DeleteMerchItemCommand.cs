using MediatR;

namespace Src.Application.Features.MerchItem.Commands;

public record DeleteMerchItemCommand(string UserId, int SubmissionId) : IRequest<bool>;

