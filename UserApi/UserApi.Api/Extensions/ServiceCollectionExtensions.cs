using UserApi.Applications.Interfaces;
using UserApi.Applications.Services;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Repositories;

namespace UserApi.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });

            services.AddAutoMapperConfiguration();

            return services;
        }
    }
}