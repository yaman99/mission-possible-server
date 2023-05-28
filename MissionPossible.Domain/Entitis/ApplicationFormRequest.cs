using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Entitis
{
    public class ApplicationFormRequest : BaseEntity<Guid>
    {
        public Guid StudentId { get; set; }
        public string TranscriptUrl { get; set; }
        public string ApplicationFormUrl { get; set; }
        public string SgkUrl { get; set; }
        public string Status { get; set; }
        public string RejectMessage { get; set; }
        public ApplicationFormRequest(Guid id) : base(id)
        {
        }
    }
}
