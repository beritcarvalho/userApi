using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sib_api_v3_sdk.Client;
using System.Globalization;
using UserApi.Applications.Interfaces;
using UserApi.Applications.Mappings;
using UserApi.Applications.Services;
using UserApi.Applications.Validators;
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
            services.AddTransient<TokenService>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddControllers()
                .AddFluentValidation(config => {
                    config.RegisterValidatorsFromAssemblyContaining <AccountInputModelValidator>();
                    config.RegisterValidatorsFromAssemblyContaining <ChangePasswordInputModelValidator>();
                    config.RegisterValidatorsFromAssemblyContaining <ChangeUserNameInputModelValidator>();
                    config.RegisterValidatorsFromAssemblyContaining <RecoveryPasswordInputModelValidator>();
                    config.RegisterValidatorsFromAssemblyContaining <RecoveryUserNameInputModelValidator>();
                    config.RegisterValidatorsFromAssemblyContaining <UserInputModelValidator>();
                    config.ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");
                     })
        .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });            
        }

        public static void LoadConfiguration(this IServiceCollection services, IConfiguration configuration)
        {            
            Configuration.Default.ApiKey.Add("api-key", configuration.GetSection("SendInBlue").GetSection("SendInBlueKey").Value);
            services.AddAutoMapperConfiguration();
        }

        public static void AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile));
        }
    }
}
