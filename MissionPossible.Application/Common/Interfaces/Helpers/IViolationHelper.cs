using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Interfaces.Helpers
{
    public interface IViolationHelper
    {
        Task HandleViolation(string type, int count, string email, Guid userId , Guid eventId);
    }
}
