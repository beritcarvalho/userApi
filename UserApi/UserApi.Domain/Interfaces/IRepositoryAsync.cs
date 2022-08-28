using UserApi.Domain.Entities;

namespace UserApi.Domain.Interfaces
{
    public interface IRepositoryAsync<T> where T : Entity
    {
        Task<T> InsertAsync(T entity);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entidade);
    }
}