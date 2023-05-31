using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetAsync(Guid id);
        Task AddAsync(Notification request);
        Task UpdateAsync(Notification request);
        Task<IEnumerable<Notification>> GetAllAsync(string type);
        Task<IEnumerable<Notification>> GetAllAsync(Guid user);
    }
}
