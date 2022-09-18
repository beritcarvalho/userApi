using System.Globalization;
using UserApi.Applications.Interfaces;
using UserApi.Applications.Services;
using UserApi.Applications.Validators;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Repositories;

namespace UserApi.Api.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {


            return services;
        }
    }
}