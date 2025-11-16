using MediatR;
using Src.Domain.Item;

namespace Src.Application.Features.Query.ItemMetaData;

public record ViewItemsGetRequestQuery : ViewItemsRequest, IRequest<IEnumerable<ViewItemsResponse>>;

