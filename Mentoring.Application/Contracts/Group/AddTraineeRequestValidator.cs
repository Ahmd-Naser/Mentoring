using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Group;

public class AddTraineeRequestValidator : AbstractValidator<AddTraineeRequest>
{
    public AddTraineeRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
