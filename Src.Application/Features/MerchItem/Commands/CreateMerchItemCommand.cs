using MediatR;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.Commands;

public record CreateMerchItemCommand(string User, CreateMerchItemRequest Request) : IRequest<bool>;
