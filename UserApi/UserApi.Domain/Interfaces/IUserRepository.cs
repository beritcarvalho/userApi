using UserApi.Domain.Entities;

namespace UserApi.Domain.Interfaces
{
    public interface IUserRepository : IRepositoryAsync<User>
    {
        Task<User> GetUserByIdWithIncludeAsync(int id);
        Task<User> GetUserForChangePassword(string cpf, string login, string phone);
        Task<User> GetUserForLoginForget(string cpf, string phone);
        Task<User> GetUserForChangeUserName(string cpf, string login);
    }
}
