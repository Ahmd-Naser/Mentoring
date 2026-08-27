using Mentoring.Application.Contracts.Submission;
using Mentoring.Application.Contracts.TraineeProblem;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Mapping;

public class TraineeProblemMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<TraineeProblem, TraineeProblemResponse>()
        // استخدام المعامل السحري ?. لحماية الكود من الـ null
            .Map(dest => dest.ProblemName, src => src.Problem.Name ?? string.Empty)
            .Map(dest => dest.ProblemLink, src => src.Problem.Link ?? string.Empty)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.LastStartedAt, src => src.LastStartedAt)
            .Map(dest => dest.Notes, src => src.Problem.Notes ?? null );

    }
}