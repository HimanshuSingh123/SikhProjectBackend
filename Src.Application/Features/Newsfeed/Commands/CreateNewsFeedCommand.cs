using MediatR;
using Src.Domain.NewsFeed;

namespace Src.Application.Features.Newsfeed.Commands;

public record CreateNewsFeedCommand(string Username, CreateNewsFeedRecordRequest Request) : IRequest<bool>;

