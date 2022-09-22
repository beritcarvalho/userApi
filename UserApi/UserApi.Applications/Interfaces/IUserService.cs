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
        Task<UserAddViewModel> AddUser(UserInputModel userInput);
        Task<UserViewModel> GetUserByIdWithInclude(int id);
        Task<UserActiveViewModel> ActiveUser(int id);
        Task<UserInactiveViewModel> InactiveUser(int id);
        Task<ChangeRoleViewModel> ChangeRole(int idUser, int IdRole);        
    }
}
