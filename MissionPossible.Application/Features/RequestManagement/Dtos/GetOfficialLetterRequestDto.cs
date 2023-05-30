using MissionPossible.Application.Common.Mappings;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Dtos
{
    public class GetOfficialLetterRequestDto : IMapFrom<StudentRequest>
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string TranscriptUrl { get; set; }
        public string OfficialLetterUrl { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public string InternshipType { get; set; }
    }
}
