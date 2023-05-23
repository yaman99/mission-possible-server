using MissionPossible.Application.Repository;
using MissionPossible.Features.Identity.Commands;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Validators
{
    public class UserSignUpValidator : AbstractValidator<UserSignUpCommand>
    {
        IUserRepository _userRepository;
        public UserSignUpValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        async Task<bool> NotExistEmail(string email)
        {
            var result =  !(await _userRepository.CheckEmailExist(email));
            return result;
        }
        public override Task<ValidationResult> ValidateAsync(ValidationContext<UserSignUpCommand> context, CancellationToken cancellation = default)
        {

            RuleFor(cmd => cmd.Email)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .MustAsync(async (email, ct) => await (NotExistEmail(email)))
                .WithMessage("Email in use");
            RuleFor(cmd => cmd.Password)
                .NotEmpty()
                .WithMessage("{PropertyName} should be not empty.")
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Password is Not Equal Password Confirmation");

            RuleFor(cmd => cmd.UserType)
               .NotEmpty()
               .WithMessage("{PropertyName} should be not empty.");

            RuleFor(cmd => cmd.FirstName)
               .NotEmpty()
               .WithMessage("{PropertyName} should be not empty.");

            RuleFor(cmd => cmd.LastName)
               .NotEmpty()
               .WithMessage("{PropertyName} should be not empty.");


            return base.ValidateAsync(context, cancellation);
        }
    }
}
