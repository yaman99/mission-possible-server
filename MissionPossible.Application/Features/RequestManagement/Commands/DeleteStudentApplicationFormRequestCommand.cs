using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Commands
{
    public class DeleteStudentApplicationFormRequestCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
