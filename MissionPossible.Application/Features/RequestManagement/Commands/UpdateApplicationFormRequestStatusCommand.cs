using MediatR;
using Microsoft.AspNetCore.Http;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Commands
{
    public class UpdateApplicationFormRequestStatusCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}
