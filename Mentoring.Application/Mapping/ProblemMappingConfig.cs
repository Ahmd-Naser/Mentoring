using Mapster;

using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Mapping;

public class ProblemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Problem, ProblemResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Title, src => src.Name)
            .Map(dest => dest.Link, src => src.Link)
            .Map(dest => dest.Notes, src => src.Notes);

    }
}
