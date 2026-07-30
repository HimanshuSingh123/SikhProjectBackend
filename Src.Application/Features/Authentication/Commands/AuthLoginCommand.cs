using MediatR;
using Src.Domain.Authentication;

namespace Src.Application.Features.Authentication;

public record AuthLoginCommand : LoginRequest, IRequest<string>;
