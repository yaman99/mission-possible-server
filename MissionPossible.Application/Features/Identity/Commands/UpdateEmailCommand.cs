using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class UpdateEmailCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
