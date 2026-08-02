using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mentoring.Application.Contracts.Authentication;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(l => l.email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(l => l.password).NotEmpty();
    }
}
