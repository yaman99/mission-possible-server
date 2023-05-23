using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Dtos
{
    public class CreateQuickAdvertiserAccountDto 
    {
        public string Code { get; set; }
        public Guid OwnerId { get; set; }
    }
}
