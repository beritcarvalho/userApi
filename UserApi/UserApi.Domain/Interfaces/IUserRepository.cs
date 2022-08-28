using UserApi.Domain.Entities;

namespace UserApi.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
    }
}
