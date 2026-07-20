using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Problem;

public class CreateProblemRequestValidator : AbstractValidator<CreateProblemRequest>

{
    public CreateProblemRequestValidator ()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(150).WithMessage("Name must not exceed 150 characters.");

        RuleFor(x => x.Link)
            .NotEmpty().WithMessage("Link is required.")
            .MaximumLength(500).WithMessage("Link must not exceed 500 characters.");

        RuleFor(x => x.Notes)
            .MaximumLength(1000).WithMessage("Notes must not exceed 1000 characters.");
    }
}
