using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Queries
{
    public class GetAllApplicationFormRequestQuery : IRequest<Result>
    {
        public string RequestType { get; set; }
    }
}
