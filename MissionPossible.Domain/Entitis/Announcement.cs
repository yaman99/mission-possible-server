using MissionPossible.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Domain.Entitis
{
    public class Announcement : BaseEntity<Guid>
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public Announcement(Guid id) : base(id)
        {
        }
    }
}
