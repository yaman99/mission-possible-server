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
    public class ApplicationFormRepository : IApplicationFormRepository
    {
        private readonly IMongoRepository<ApplicationFormRequest, Guid> _repository;

        public ApplicationFormRepository(IMongoRepository<ApplicationFormRequest, Guid> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(ApplicationFormRequest request)
            => await _repository.AddAsync(request);

        public async Task<IEnumerable<ApplicationFormRequest>> GetAllAsync()
            => await _repository.FindAsync(x => !x.IsDeleted);
        public async Task<IEnumerable<ApplicationFormRequest>> GetAllByStudentAsync(Guid studentId)
            => await _repository.FindAsync(x => x.StudentId == studentId && !x.IsDeleted);

        public async Task<ApplicationFormRequest> GetAsync(Guid id)
            => await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<ApplicationFormRequest> GetByStudentAsync(Guid id)
            => await _repository.GetAsync(x => x.StudentId == id && !x.IsDeleted);

        public async Task UpdateAsync(ApplicationFormRequest request)
            => await _repository.UpdateAsync(request);
    }
}
