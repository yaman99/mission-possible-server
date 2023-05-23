﻿using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Events
{
    public class AccountSuspendedForLackOfBalance : DomainEvent
    {
        public int Count { get; set; }
        public string Email { get; set; }
        public Guid User { get; set; }
    }
}
