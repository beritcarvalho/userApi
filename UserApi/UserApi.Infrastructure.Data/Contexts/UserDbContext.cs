using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Data.Configurations.EntityConfigurations;

namespace UserApi.Infrastructure.Data.Contexts
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AccountConfiguration());
        }
    }
}
