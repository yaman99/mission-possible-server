using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Shared.Types
{
    public class PagedQuery : PagedQueryBase
    {
        public PagedQuery(int page, int results, string orderBy) 
            : base(page, results, orderBy)
        {

        }
    }
}
