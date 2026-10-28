using MediatR;
using Src.Domain.NewsFeed;

namespace Src.Application.Features.Newsfeed.Commands;

public record UpdateNewsfeedCommand(String Username, UpdateNewsFeedRequest Request) : IRequest<bool>;

