using MediatR;
using Src.Domain.Item;
using Src.Dto.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Query.ItemMetaData;

public record ItemMetaDataGetRequestQuery : ItemMetaDataRequest,  IRequest<ItemMetaDataResponse>;


