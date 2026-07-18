using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
{
    public CreateGroupRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Group name is required.")
            .MinimumLength(3).WithMessage("Group name must be at least 3 characters long.")
            .MaximumLength(100).WithMessage("Group name cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}