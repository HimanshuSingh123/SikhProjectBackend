using Mapster;
using Src.Application.Features.Authentication.Commands;
using Src.Dto.Authentication;

namespace Src.Api.MappingProfile;

public class AuthenticationProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequestDto, AuthLoginCommand>();
        config.NewConfig<RegisterRequestDto, AuthRegisterCommand>();
    }
}

