using MissionPossible.Application.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.Admin.AddNewAdmin
{
    public class AddNewAdminCommand : IRequest<Result>
    {
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
