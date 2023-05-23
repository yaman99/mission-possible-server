using MissionPossible.Domain.Common;
using MissionPossible.Infrastructure.Mongo;
using MissionPossible.Shared;
using Autofac;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace MissionPossible.Infrastructure
{
    public static class DependencyInjection
    {
       
        public static void AddMongo(this ContainerBuilder builder)
        {
            builder.Register(context =>
            {
                var configuration = context.Resolve<IConfiguration>();
                var options = configuration.GetOptions<MongoDbOptions>("mongo");

                return options;
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();

                return new MongoClient(options.ConnectionString);
            }).SingleInstance();

            builder.Register(context =>
            {
                var options = context.Resolve<MongoDbOptions>();
                var client = context.Resolve<MongoClient>();
                return client.GetDatabase(options.Database);

            }).InstancePerLifetimeScope();
        }

        public static void AddMongoRepository<TEntity, T>(this ContainerBuilder builder, string collectionName)
            where TEntity : IIdentifiable<T>
            => builder.Register(ctx => new MongoRepository<TEntity, T>(ctx.Resolve<IMongoDatabase>(), collectionName))
                .As<IMongoRepository<TEntity, T>>()
                .InstancePerLifetimeScope();
    }
}
