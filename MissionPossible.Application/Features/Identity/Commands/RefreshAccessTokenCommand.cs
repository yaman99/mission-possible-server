using MissionPossible.Application.Common.Models;
using MediatR;

namespace MissionPossible.Features.Identity.Commands
{
    public class RefreshAccessTokenCommand : IRequest<Result>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
