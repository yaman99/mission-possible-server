namespace MissionPossible.Infrastructure.Mongo
{
    public class MongoDbOptions
    {
        public string Database { get; set; }
        public bool Seed { get; set; }
        public string ConnectionString { get; set; }
    }
}