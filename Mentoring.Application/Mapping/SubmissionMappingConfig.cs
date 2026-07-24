using Mapster;
using Mentoring.Application.Contracts.Group;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Mapping;

public class SubmissionMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<Submission, SubmissionResponse>()
        // استخدام المعامل السحري ?. لحماية الكود من الـ null
            .Map(dest => dest.CreatedAt, src => src.SubmittedAt);

    }
}