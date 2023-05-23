using MissionPossible.Application.Repository;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Features.Identity.Commands;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace MissionPossible.Features.Identity.Validators
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
        {
            RuleFor(cmd => cmd.CurrentPassword)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.");
            RuleFor(cmd => cmd.NewPassword)
                .NotEmpty().WithMessage("{PropertyName} should be not empty.")
                .Length(8, 30);
            RuleFor(cmd => cmd.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.NewPassword).WithMessage("New Password is Not Equal Password Confirmation");
        }
        //private async Task<bool> IsItOldPassword(Guid userId, string newPassword)
        //{
        //    var user = await _userRepository.GetAsync(userId);
        //    return !user.ValidatePassword(newPassword, _passwordHasher);
        //}
    }
}
