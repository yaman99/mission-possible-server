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
    public class AddApplicationFormRequestCommand : IRequest<Result>
    {
        public IFormFile Transcript { get; set; }
        public IFormFile ApplicationForm { get; set; }
        public string InternshipType { get; set; }
    }
}
