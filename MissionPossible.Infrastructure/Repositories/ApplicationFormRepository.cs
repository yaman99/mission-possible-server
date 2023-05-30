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
        private readonly IMongoRepository<StudentRequest, Guid> _repository;

        public ApplicationFormRepository(IMongoRepository<StudentRequest, Guid> repository)
        {
            _repository = repository;
        }

        public async Task AddAsync(StudentRequest request)
            => await _repository.AddAsync(request);

        public async Task<IEnumerable<StudentRequest>> GetAllAsync(string requestType)
            => await _repository.FindAsync(x => x.RequestType == requestType && !x.IsDeleted);
        public async Task<IEnumerable<StudentRequest>> GetAllAsync(string requestType, string status)
            => await _repository.FindAsync(x => x.RequestType == requestType && x.Status == status && !x.IsDeleted);
        public async Task<IEnumerable<StudentRequest>> GetAllByStudentAsync(Guid studentId , string requestType)
            => await _repository.FindAsync(x => x.StudentId == studentId && x.RequestType == requestType && !x.IsDeleted);

        public async Task<StudentRequest> GetAsync(Guid id)
            => await _repository.GetAsync(x => x.Id == id && !x.IsDeleted);

        public async Task<StudentRequest> GetByStudentAsync(Guid id)
            => await _repository.GetAsync(x => x.StudentId == id && !x.IsDeleted);

        public async Task UpdateAsync(StudentRequest request)
            => await _repository.UpdateAsync(request);
    }
}
