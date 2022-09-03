using UserApi.Applications.Interfaces;
using UserApi.Applications.Mappings;
using UserApi.Applications.Services;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Repositories;

namespace UserApi.Api.Extensions
{
    public static class AutoMapperConfig
    {
        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }
    }
}