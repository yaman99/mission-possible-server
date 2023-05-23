using MissionPossible.Application.Common.Mappings;
using MissionPossible.Application.Common.Models;
using MissionPossible.Domain.Entities.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.UpdateUser
{
    public class UpdateUserCommand : IRequest<Result> , IMapFrom<User>
    {
        public Guid Id { get; set; }
        public string Email { get;  set; }
        public string? Phone { get;  set; }
        public bool IsActive { get;  set; }
        public bool IsDeleted { get; set; }
    }
}
