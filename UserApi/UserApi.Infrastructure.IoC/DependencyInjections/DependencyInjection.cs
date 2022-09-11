using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserApi.Applications.Interfaces;
using UserApi.Applications.Services;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Contexts;
using UserApi.Infrastructure.Data.Repositories;

namespace UserApi.Infrastructure.IoC.DependencyInjections
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region DataBaseConnection
            services.AddDbContext<UserDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            #endregion


            AddRepositoriesInfra(services);
            AddServicesInfra(services);

            return services;
        }

        private static void AddRepositoriesInfra(IServiceCollection services)
        {
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }

        public static void AddServicesInfra(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IRecoveryService, RecoveryService>();
        }
    }
}
