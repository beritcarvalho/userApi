using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IRoleService
    {
        Task<RoleViewModel> GetRoleById(int id);
        Task<List<RoleViewModel>> GetRoleAll();
    }
}
