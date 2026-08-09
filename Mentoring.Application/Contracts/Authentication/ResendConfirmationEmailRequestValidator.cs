using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

internal class ResendConfirmationEmailRequestValidator : AbstractValidator<ResendConfirmationEmailRequest>
{
    public ResendConfirmationEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
