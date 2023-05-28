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
        Task<ApplicationFormRequest> GetAsync(Guid id);
        Task<ApplicationFormRequest> GetByStudentAsync(Guid id);
        Task AddAsync(ApplicationFormRequest request);
        Task UpdateAsync(ApplicationFormRequest request);
        Task<IEnumerable<ApplicationFormRequest>> GetAllAsync();
        Task<IEnumerable<ApplicationFormRequest>> GetAllByStudentAsync(Guid studentId);
    }
}
