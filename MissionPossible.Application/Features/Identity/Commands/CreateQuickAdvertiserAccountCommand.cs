using MissionPossible.Application.Features.Identity.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class CreateQuickAdvertiserAccountCommand : IRequest<CreateQuickAdvertiserAccountDto>
    {
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
