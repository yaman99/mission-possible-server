using MissionPossible.Application.Common.Mappings;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.AnnouncementManager.Dtos
{
    public class GetAllAnnouncementsDto : IMapFrom<Announcement>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
    }
}
