using Mentoring.Application.Contracts.Authentication;

namespace Mentoring.Application.Mapping;

public class UserMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);
    }
}
