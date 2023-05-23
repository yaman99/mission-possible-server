using MissionPossible.Application.Common.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace MissionPossible.Features.Identity.Commands
{
    public class ChangePasswordCommand : IRequest<Result>
    {
        public string CurrentPassword { get; }
        public string NewPassword { get; }
        public string ConfirmPassword { get; }

        [JsonConstructor]
        public ChangePasswordCommand(
            string currentPassword,
            string newPassword,
            string confirmPassword)
        {
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
            ConfirmPassword = confirmPassword;
        }
    }
}
