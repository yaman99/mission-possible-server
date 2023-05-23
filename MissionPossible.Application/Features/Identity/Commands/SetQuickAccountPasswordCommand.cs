using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Commands
{
    public class SetQuickAccountPasswordCommand : IRequest<Result>
    {
        public string VerificationCode { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
