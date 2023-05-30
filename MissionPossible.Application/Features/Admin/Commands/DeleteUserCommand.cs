using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.Commands
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
    }
}
