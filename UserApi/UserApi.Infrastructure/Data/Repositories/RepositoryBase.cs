using Microsoft.EntityFrameworkCore;
using UserApi.Domain.Entities;
using UserApi.Infrastructure.Data.Contexts;

namespace UserApi.Infrastructure.Data.Repositories
{
    public class BaseRepository<T> where T : Entity
    {
        protected readonly UserDbContext Context;

        public BaseRepository(UserDbContext context) => Context = context;

        //Create
        public T Insert(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            Context.Set<T>().AddAsync(entity);
            Context.SaveChangesAsync();
            return entity;
        }

        //Read
        public T GetById(decimal id)
        {
            var entity = Context.Set<T>().Find(id);
            return entity;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await Context.Set<T>().FindAsync(id);
            return entity;
        }

        public List<T> GetAll() => Context.Set<T>().ToList();

        public Task<List<T>> GetAllAsync() => Context.Set<T>().ToListAsync();

        //Update
        public T Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChangesAsync();
            return entity;
        }

        //Delete
        public void Remove(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            Context.SaveChanges();
        }

        public async Task RemoveAsync(T entidade)
        {
            Context.Set<T>().Remove(entidade);
            await Context.SaveChangesAsync();
        }
    }
}