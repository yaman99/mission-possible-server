using MissionPossible.Application.Common.Models;
using MediatR;

namespace MissionPossible.Features.Identity.Commands
{
    public class UserSignOutCommand : IRequest<Result>
    {
        public string AccessToken { get; set; }
    }
}
