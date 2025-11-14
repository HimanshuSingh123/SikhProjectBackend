using MediatR;
using Src.Domain.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Query.ItemMetaData;

public record ItemMetaDataGetRequest : ItemMetaDataRequest,  IRequest<ItemMetaDataResponse>;


