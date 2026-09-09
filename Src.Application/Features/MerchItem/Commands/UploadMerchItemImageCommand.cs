using MediatR;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.Commands;

public record UploadMerchItemImageCommand(string User, UploadImageMerchItemRequest Request) : IRequest<bool>;
