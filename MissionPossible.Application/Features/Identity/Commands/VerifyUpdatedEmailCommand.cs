using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class VerifyUpdatedEmailCommand : IRequest<Result>
    {
        public string ActivationCode { get; set; }

        public VerifyUpdatedEmailCommand(string activationCode)
        {
            ActivationCode = activationCode;
        }
    }
}
