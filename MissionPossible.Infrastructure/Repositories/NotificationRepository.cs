using MissionPossible.Application.Common.Interfaces.Repositories;
using MissionPossible.Domain.Entitis;
using MissionPossible.Infrastructure.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IMongoRepository<Notification, Guid> _repository;

        public NotificationRepository(IMongoRepository<Notification, Guid> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Notification request)
            => await _repository.AddAsync(request);
        public async Task<IEnumerable<Notification>> GetAllAsync(string type)
            => await _repository.FindAsync(x => x.Type == type && !x.IsDeleted);
        public async Task<IEnumerable<Notification>> GetAllAsync(Guid user)
            => await _repository.FindAsync(x => x.UserId == user && !x.IsDeleted);
        public async Task<Notification> GetAsync(Guid id)
            => await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

        public async Task UpdateAsync(Notification request)
            => await _repository.UpdateAsync(request);
    }
}
