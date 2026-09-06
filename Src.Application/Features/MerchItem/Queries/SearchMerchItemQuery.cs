using MediatR;
using Src.Domain.MerchItems;

namespace Src.Application.Features.MerchItem.Queries;

public record SearchMerchItemQuery(string User, SearchMerchItemRequest Request) : IRequest<SearchMerchItemResponse?>;

