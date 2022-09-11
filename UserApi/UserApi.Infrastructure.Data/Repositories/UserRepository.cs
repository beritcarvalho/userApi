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

        public async Task<User> GetUserByIdWithIncludeAsync(int id)
        {
            var user = await Context
                .Users
                .Where(user => user.Id == id)
                .Include(user => user.Account)
                .Include(user => user.Role)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserForChangePassword(string cpf, string login, string phone)
        {
            var user = await Context
                .Users
                .Where(user => user.Login == login && user.Account.Cpf == cpf && phone == phone)
                .Include(user => user.Account)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserForLoginForget(string cpf, string phone)
        {
            var user = await Context
                .Users
                .Where(user => user.Account.Cpf == cpf && user.Account.Phone == phone)
                .Include(user => user.Account)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User> GetUserForChangeUserName(string cpf, string login)
        {
            var user = await Context
                .Users
                .Where(user => user.Account.Cpf == cpf && user.Login == login)
                .Include(user => user.Account)
                .FirstOrDefaultAsync();

            return user;
        }
    }
}
