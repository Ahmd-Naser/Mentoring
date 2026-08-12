using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public class ForgetPasswordRequestValidator : AbstractValidator<ForgetPasswordRequest>
{
    public ForgetPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
