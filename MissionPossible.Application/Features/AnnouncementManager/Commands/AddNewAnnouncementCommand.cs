using MediatR;
using MissionPossible.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.AnnouncementManager.Commands
{
    public class AddNewAnnouncementCommand : IRequest<Result>
    {
        public string Title{ get; set; }
        public string Url{ get; set; }
        public string Content{ get; set; }
    }
}
