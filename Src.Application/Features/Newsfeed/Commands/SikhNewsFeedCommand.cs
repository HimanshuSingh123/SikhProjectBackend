using MediatR;

namespace Src.Application.Features.Newsfeed.Commands;

public record SikhNewsFeedCommand(String Username) : IRequest<bool>;


