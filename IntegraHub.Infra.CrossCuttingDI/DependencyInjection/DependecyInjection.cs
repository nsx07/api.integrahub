using IntegraHub.Domain.Entities;
using IntegraHub.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using IntegraHub.Service.Services;
using IntegraHub.Infra.Data.Repository;
using Microsoft.Extensions.Configuration;
using IntegraHub.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using IntegraHub.Infra.CrossCutting.Utils;
using IntegraHub.Infra.Data.Queries;
using IntegraHub.Infra.Data.Subscriptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using IntegraHub.Service.Services.Auth;

namespace IntegraHub.Infra.CrossCuttingDI.DependencyInjection
{
    public class DependecyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<CurrentClientResolver>();

            ConfigureDb(services, configuration);
            ConfigureServices(services);
            ConfigureRepositories(services);
        }

        public static void ConfigureDb(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PostgresContext>(options =>
            {
                options.UseNpgsql(VarEnvHelper.GetConnectionString("postgresql"), opt =>
                {
                    opt.CommandTimeout(180);
                    opt.EnableRetryOnFailure(5);
                });
            });

            services
                .AddGraphQLServer()
                .InitializeOnStartup()
                .RegisterDbContext<PostgresContext>()
                .AddMaxExecutionDepthRule(100)
                .AddSubscriptionType<Subscriptions>()
                .AddQueryType<Query>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICompanyService, CompanyService>();
            services.AddTransient<IEnvironmentIntegrationService, EnvironmentIntegrationService>();
            services.AddScoped<IAuthService, AuthService>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
        }
    }
}
