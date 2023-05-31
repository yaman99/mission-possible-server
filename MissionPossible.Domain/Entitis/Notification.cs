using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Entitis
{
    public class Notification : BaseEntity<Guid>
    {
        public string Message { get; set; }
        public string Type { get; set; }
        public Guid UserId { get; set; }
        public Notification(Guid id) : base(id)
        {
        }
    }
}
