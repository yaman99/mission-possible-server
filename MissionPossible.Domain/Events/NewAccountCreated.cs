using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Events
{
    public class NewAccountCreated : DomainEvent
    {
        public string Email { get; set; }
        public string ActivationCode { get; set; }

        public NewAccountCreated(string email , string activationCode)
        {
            ActivationCode = activationCode;
            Email = email;
        }
    }
}
