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
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        public PersonRepository(UserDbContext context) : base(context)
        {
        }
    }
}
