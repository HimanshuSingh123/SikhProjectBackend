using MediatR;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.Commands;

public record SaveMerchItemCommand(string User, SaveMerchItemRequest Request) : IRequest<bool>;

