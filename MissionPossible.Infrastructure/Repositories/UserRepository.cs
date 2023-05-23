using MissionPossible.Application.Repository;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Infrastructure.Mongo;
using MissionPossible.Shared.Types;
using MongoDB.Driver;

namespace MissionPossible.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoRepository<User, Guid> _repository;

        public UserRepository(IMongoRepository<User, Guid> repository)
        {
            _repository = repository;
        }

        public async Task<bool> CheckEmailExist(string email)
            => await _repository.ExistsAsync(x => x.Email == email && !x.IsDeleted);


        public async Task<User> GetAsync(Guid id)
            => await _repository.GetAsync(x=>x.Id == id && !x.IsDeleted);

        public async Task<User> GetAsync(string email)
            => await _repository.GetAsync(x => x.Email == email.ToLowerInvariant() );

        public async Task AddAsync(User user)
            => await _repository.AddAsync(user);

        public async Task UpdateAsync(User user)
            => await _repository.UpdateAsync(user);

        public async Task<PagedResult<User>> GetAllPagedAsync(string type, PagedQueryBase page)
            => await _repository.BrowseAsync(x => x.UserType == type , o => o.CreatedDate, true, page);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await _repository.FindAsync(x => !x.IsDeleted);

        public async Task<User> GetUserByActivation(string activation)
        {
            return await _repository.GetAsync(item => item.ActivationCode == activation);
        }
    }

}
