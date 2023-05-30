using MissionPossible.Application.Common.Mappings;
using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Features.RequestManagement.Dtos
{
    public class GetApplicationFormRequestDto : IMapFrom<StudentRequest>
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public string TranscriptUrl { get; set; }
        public string ApplicationFormUrl { get; set; }
        public string SgkUrl { get; set; }
        public string Status { get; set; }
        public string RejectMessage { get; set; }
        public string InternshipType { get; set; }
    }
}
