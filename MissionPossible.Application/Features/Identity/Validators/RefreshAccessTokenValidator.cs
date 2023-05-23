using MissionPossible.Features.Identity.Commands;
using FluentValidation;

namespace AutoNinja.Server.Application.Features.Authentication.Validators
{
    public class RefreshAccessTokenValidator : AbstractValidator<RefreshAccessTokenCommand>
    {
        public RefreshAccessTokenValidator()
        {
            RuleFor(cmd => cmd.AccessToken)
                .NotEmpty()
               .WithMessage("{PropertyName} should be not empty.");
            RuleFor(cmd => cmd.RefreshToken)
                .NotEmpty()
               .WithMessage("{PropertyName} should be not empty.");
        }
    }
}