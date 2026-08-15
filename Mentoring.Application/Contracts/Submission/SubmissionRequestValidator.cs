using FluentValidation;
using Mentoring.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Submission;

public class SubmissionRequestValidator : AbstractValidator<SubmissionRequest>
{
    public SubmissionRequestValidator()
    {
        RuleFor(s => s.CodeLink)
            .NotNull()
            .NotEmpty()
            .Length(3, 200);

        RuleFor(s => s.Verdict)
            .NotNull()
            .IsInEnum();

    }
}
