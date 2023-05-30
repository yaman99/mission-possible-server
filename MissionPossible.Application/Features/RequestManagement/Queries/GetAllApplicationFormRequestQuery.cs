using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Queries
{
    public class GetAllRequestsQuery : IRequest<Result>
    {
        public string RequestType { get; set; }
        public string Status { get; set; }
    }
}
