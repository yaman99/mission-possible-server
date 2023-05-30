using MissionPossible.Domain.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionPossible.Application.Common.Interfaces.Repositories
{
    public interface IApplicationFormRepository
    {
        Task<StudentRequest> GetAsync(Guid id);
        Task<StudentRequest> GetByStudentAsync(Guid id);
        Task AddAsync(StudentRequest request);
        Task UpdateAsync(StudentRequest request);
        Task<IEnumerable<StudentRequest>> GetAllAsync(string requestType);
        Task<IEnumerable<StudentRequest>> GetAllByStudentAsync(Guid studentId , string requestType);
    }
}
