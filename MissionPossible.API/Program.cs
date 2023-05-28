using MissionPossible.API;
using MissionPossible.Appilication;
using MissionPossible.Domain.Entities.Auth;
using MissionPossible.Infrastructure;
using MissionPossible.Shared.Authentication;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using System.Reflection;
using MissionPossible.Domain.Entitis;

var builder = WebApplication.CreateBuilder(args);

// Inject Other Layers
builder.Services.AddAppilication();
builder.Services.AddJwt();
builder.Services.AddApi(builder.Configuration);


builder.Services.AddHsts(options =>
{
    options.Preload = true;
    options.IncludeSubDomains = true;
});

// autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(cb =>
    {
        cb.RegisterAssemblyTypes(Assembly.GetEntryAssembly()!, Assembly.Load("MissionPossible.Application"),
                Assembly.Load("MissionPossible.Infrastructure"))
                    .AsImplementedInterfaces();

        cb.AddMongo();
        cb.AddMongoRepository<User, Guid>("Users");
        cb.AddMongoRepository<ApplicationFormRequest, Guid>("ApplicationFormRequests");
        cb.RegisterType<PasswordHasher<User>>().As<IPasswordHasher<User>>();
    });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Test01", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
            }
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors(x => x.WithOrigins(app.Configuration["AppSettings:Client_Url"].ToString())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials());

app.MapControllers();

app.Run();
