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
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly IMongoRepository<Announcement, Guid> _repository;

        public AnnouncementRepository(IMongoRepository<Announcement, Guid> repository)
        {
            _repository = repository;
        }
        public async Task AddAsync(Announcement request)
            => await _repository.AddAsync(request);
        public async Task<IEnumerable<Announcement>> GetAllAsync( )
            => await _repository.FindAsync(x => !x.IsDeleted);

        public async Task<Announcement> GetAsync(Guid id)
            => await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

        public async Task UpdateAsync(Announcement request)
            => await _repository.UpdateAsync(request);
    }
}
