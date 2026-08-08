using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Mapping;

public class UserMappingConfig : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, ApplicationUser>()
            .Map(dest => dest.UserName, src => src.Email);
    }
}
