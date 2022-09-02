using UserApi.Applications.Interfaces;
using UserApi.Applications.Services;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Repositories;

namespace UserApi.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}