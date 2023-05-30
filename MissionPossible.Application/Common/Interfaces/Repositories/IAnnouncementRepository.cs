using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Interfaces.Repositories
{
    public interface IAnnouncementRepository
    {
        Task<Announcement> GetAsync(Guid id);
        Task AddAsync(Announcement request);
        Task UpdateAsync(Announcement request);
        Task<IEnumerable<Announcement>> GetAllAsync();
    }
}
