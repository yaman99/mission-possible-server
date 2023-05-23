using MissionPossible.Application.Features.Identity.Commands;
using MissionPossible.Application.Repository;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Features.Identity.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace MissionPossible.Features.Identity.Validators
{
    public class SetQuickAccountPasswordValidator : AbstractValidator<SetQuickAccountPasswordCommand>
    {
        public SetQuickAccountPasswordValidator()
        {
            RuleFor(cmd => cmd.VerificationCode)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(cmd => cmd.Password)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Length(8, 30);
            RuleFor(cmd => cmd.PasswordConfirmation)
                .NotEmpty()
                .Equal(x => x.Password).WithMessage("New Password is Not Equal Password Confirmation");
        }

    }
}
