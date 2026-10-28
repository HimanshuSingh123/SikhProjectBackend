using MediatR;
using Src.Domain.Newsfeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Newsfeed.Queries;

public record GetNewsFeedListQuery(string Username, int pageNumber, int pageSize) : IRequest<IReadOnlyList<GetNewsfeedResponse>>;

