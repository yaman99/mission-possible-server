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
    public class UploadFileToRequestCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }
        public string Type { get; set; }
    }
}
