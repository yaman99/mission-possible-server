using MissionPossible.Application.Features.Admin.GetAllUsers.Dtos;
using MissionPossible.Shared.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<PagedResult<GetAllUsersDto>>
    {
        public PagedQueryBase Pagination { get; set; }
        public string UserType { get; set; }
    }
}
