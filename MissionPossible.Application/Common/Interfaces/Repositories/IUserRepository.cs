using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Shared.Types;

namespace MissionPossible.Application.Repository
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExist(string email);
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<PagedResult<User>> GetAllPagedAsync(string type , PagedQueryBase page);
        Task<User> GetUserByActivation(string activation);
    }
}
