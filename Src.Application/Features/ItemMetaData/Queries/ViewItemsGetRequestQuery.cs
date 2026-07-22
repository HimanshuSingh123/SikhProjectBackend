using MediatR;
using Src.Domain.Item;

namespace Src.Application.Features.ItemMetaData.Queries;

public record ViewItemsGetRequestQuery : ViewItemsRequest, IRequest<IEnumerable<ViewItemsResponse>>;

