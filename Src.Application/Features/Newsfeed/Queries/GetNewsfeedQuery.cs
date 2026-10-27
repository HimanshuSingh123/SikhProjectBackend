using MediatR;
using Src.Domain.Newsfeed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Src.Application.Features.Newsfeed.NewFolder;

public record GetNewsfeedQuery(string Username, int SubmissionId) : IRequest<GetNewsfeedResponse?>;


