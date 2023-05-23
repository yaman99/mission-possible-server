using MissionPossible.Application.Common.Mappings;
using MissionPossible.Application.Common.Models;
using MissionPossible.Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class UpdatePhoneCommand : IRequest<Result>
    {
        public string Phone { get; set; }
    }
}
