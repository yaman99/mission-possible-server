using MissionPossible.Application.Features.Identity.Commands;
using MissionPossible.Application.Repository;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Validators
{
    public class CreateQuickAdvertiserAccountValidator : AbstractValidator<CreateQuickAdvertiserAccountCommand>
    {
        IUserRepository _userRepository;

        public CreateQuickAdvertiserAccountValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<bool> NotExistEmail(string email)
        {
            var result = !(await _userRepository.CheckEmailExist(email));
            return result;
        }
        public override Task<ValidationResult> ValidateAsync(ValidationContext<CreateQuickAdvertiserAccountCommand> context, CancellationToken cancellation = default)
        {

            RuleFor(cmd => cmd.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .MustAsync(async (email, ct) => await (NotExistEmail(email)))
                .WithMessage("Email in use");

            return base.ValidateAsync(context, cancellation);
        }
    }
}
