using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class ActivateAccountCommand : IRequest<Result>
    {
        public ActivateAccountCommand(string activationCode)
        {
            ActivationCode = activationCode;
        }

        public string ActivationCode { get; set; }
    }
}
