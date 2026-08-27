using MediatR;
using Src.Domain.Authentication;

namespace Src.Application.Features.Authentication.Commands;

public record AuthRegisterCommand : RegisterRequest, IRequest<string>;


