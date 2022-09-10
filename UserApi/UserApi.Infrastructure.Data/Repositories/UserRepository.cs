using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Domain.Entities;
using UserApi.Domain.Interfaces;
using UserApi.Infrastructure.Data.Contexts;

namespace UserApi.Infrastructure.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected readonly UserDbContext Context;
        public UserRepository(UserDbContext context) : base(context)
        {
            Context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await Context
                .Users
                .Where(user => user.Id == id)
                .Include(user => user.Account)
                .Include(user => user.Role)
                .FirstOrDefaultAsync(user => user.Id == id);

            return user;
        }
    }
}
