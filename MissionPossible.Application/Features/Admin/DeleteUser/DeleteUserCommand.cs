using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.DeleteUser
{
    public class DeleteUserCommand : IRequest<Result>
    {
        public Guid UserId { get; set; }
    }
}
