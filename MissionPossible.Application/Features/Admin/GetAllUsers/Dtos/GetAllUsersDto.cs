using MissionPossible.Application.Common.Mappings;
using MissionPossible.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.GetAllUsers.Dtos
{
    public class GetAllUsersDto : IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
