using UserApi.Domain.Entities;

namespace UserApi.Domain.Interfaces
{
    public interface IPersonRepository : IRepositoryAsync<Person>
    {
        void Teste();
    }
}
