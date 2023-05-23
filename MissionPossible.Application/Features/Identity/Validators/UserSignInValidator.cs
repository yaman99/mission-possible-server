using MissionPossible.Features.Identity.Commands;
using FluentValidation;


namespace AutoNinja.Server.Application.Features.Authentication.Validators
{
    public class UserSignInValidator: AbstractValidator<UserSignInCommand>
    {
        public UserSignInValidator()
        {
            RuleFor(cmd => cmd.Email)
              .NotEmpty()
              .WithMessage("{PropertyName} should be not empty.")
              .EmailAddress()
              .WithMessage("Invalid email address");

            RuleFor(cmd => cmd.Password)
              .NotEmpty()
              .WithMessage("{PropertyName} should be not empty.");
        }
    }
}
