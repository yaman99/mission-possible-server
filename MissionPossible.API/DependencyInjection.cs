using MissionPossible.Api.Filters;
using MissionPossible.API.Services;
using MissionPossible.Application.Common.Interfaces;
using MissionPossible.Application.Common.Interfaces.Helpers;
using MissionPossible.Application.Features.Identity.Validators;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MissionPossible.Shared.Services;
using MissionPossible.Shared.Types;

namespace MissionPossible.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services, IConfiguration configuration)
        {

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();


            services.AddHttpContextAccessor();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            services.Configure<SmtpConfig>(configuration.GetSection("SmtpConfig"));

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            }).AddFluentValidation();


          

            return services;
        }
    }
}
