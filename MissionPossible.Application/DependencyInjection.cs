using MissionPossible.Application.Common.Behaviours;
using MissionPossible.Application.Common.Helpers;
using MissionPossible.Application.Common.Interfaces.Helpers;
using MissionPossible.Application.EventBus;
using MissionPossible.Application.EventBus.Bus;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace MissionPossible.Appilication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAppilication(this IServiceCollection services)
        {
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient<IDomainBus, MediatrBus>();
            services.AddScoped<IViolationHelper, ViolationHelper>();

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            // register logging behavior
            return services;
        }
    }
}
