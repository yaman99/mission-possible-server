using MissionPossible.Shared.Authentication;
using MediatR;
using System.Text.Json.Serialization;

namespace MissionPossible.Features.Identity.Commands
{
    public class UserSignInCommand : IRequest<JsonWebToken>
    {
        public string Email { set; get; }
        public string Password { set; get; }

        [JsonConstructor]
        public UserSignInCommand(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
