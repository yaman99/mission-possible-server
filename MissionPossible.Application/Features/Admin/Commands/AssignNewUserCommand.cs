using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.Commands
{
    public class AssignNewUserCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
    }
}
