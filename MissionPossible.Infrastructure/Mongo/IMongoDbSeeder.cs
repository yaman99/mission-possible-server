using System.Threading.Tasks;

namespace MissionPossible.Infrastructure.Mongo
{
    public interface IMongoDbSeeder
    {
        Task SeedAsync();
    }
}