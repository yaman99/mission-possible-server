using MissionPossible.Application.Common.Mappings;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Domain.Entitis.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Identity.Dtos
{
    public class GetUserDto : IMapFrom<User>
    {
        public string Email { get; private set; }
        public string FullName { get; private set; }
        public string UserType { get; set; }
    }
    
}
