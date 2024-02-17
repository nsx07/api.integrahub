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

namespace IntegraHub.Infra.CrossCuttingDI.DependencyInjection
{
    public class DependecyInjection
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
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
                .RegisterDbContext<PostgresContext>()
                .AddMaxExecutionDepthRule(100)
                .AddQueryType<Query>()
                .AddProjections()
                .AddFiltering()
                .AddSorting();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEnvironmentIntegrationService, EnvironmentIntegrationService>();
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
