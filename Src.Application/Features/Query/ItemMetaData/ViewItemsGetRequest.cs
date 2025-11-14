using MediatR;
using Src.Domain.Item;

namespace Src.Application.Features.Query.ItemMetaData;

public record ViewItemsGetRequest : ViewItemsRequest, IRequest<IEnumerable<ViewItemsResponse>>;

