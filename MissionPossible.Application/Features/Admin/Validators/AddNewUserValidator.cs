using FluentValidation;
using FluentValidation.Results;
using MissionPossible.Application.Features.Admin.Commands;
using MissionPossible.Application.Repository;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.Validators
{
    public class AddNewUserValidator : AbstractValidator<AssignNewUserCommand>
    {
        readonly IUserRepository _userRepository;
        public AddNewUserValidator(IUserRepository repo)
        {
            _userRepository = repo;
            
        }

        async Task<bool> NotExistEmail(string email)
        {
            var result = !(await _userRepository.CheckEmailExist(email));
            return result;
        }
        public override Task<ValidationResult> ValidateAsync(ValidationContext<AssignNewUserCommand> context, CancellationToken cancellation = default)
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
