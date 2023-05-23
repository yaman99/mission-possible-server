using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Entitis.Auth
{
    public class Violation
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public int RepeatedCount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
