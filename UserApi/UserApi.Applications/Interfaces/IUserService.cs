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
        Task<UserViewModel> GetUserById(int id);
        Task<UserViewModel> AddUser(UserInputModel accountInput);
        Task<UserViewModel> UpdateUser(UpdateUserInputModel accountInput);
        Task<UserViewModel> RemoveUserById(int id);
    }
}
