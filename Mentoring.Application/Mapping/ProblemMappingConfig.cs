using Mentoring.Application.Contracts.Problem;

namespace Mentoring.Application.Mapping;

public class ProblemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Problem, ProblemResponse>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Link, src => src.Link)
            .Map(dest => dest.Notes, src => src.Notes)
            .Map(dest => dest.Difficulty, src => src.Difficulty);

    }
}
