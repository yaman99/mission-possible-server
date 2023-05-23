using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Events
{
    public class NewAccountWithTempPasswordCreated : DomainEvent
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
