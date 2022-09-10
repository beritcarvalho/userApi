using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetUserByIdWithInclude(int id);
        Task<UserAddViewModel> AddUser(UserInputModel accountInput);
        Task<UserViewModel> UpdateUser(UserInputModel accountInput);
        Task<UserViewModel> RemoveUserById(int id);
    }
}
