using MissionPossible.Application.Common.Models;
using MissionPossible.Application.Features.Identity.Dtos;
using MediatR;

namespace MissionPossible.Features.Identity.Commands
{
    public class UserSignUpCommand : IRequest<UserSignUpDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
    }
}
