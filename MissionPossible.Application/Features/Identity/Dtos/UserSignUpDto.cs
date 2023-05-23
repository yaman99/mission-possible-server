using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Dtos
{
    public class UserSignUpDto
    {
        public string  AcitivationCode { get; set; }
        public Guid OwnerId { get; set; }
    }
}
