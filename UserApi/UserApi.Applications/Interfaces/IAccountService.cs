using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IAccountService
    {
        Task<AccountViewModel> GetById(int id);
        Task<AccountViewModel> AddAccount(AccountInputModel accountInput);
        Task<AccountViewModel> UpdateAccount(AccountInputModel accountInput);
        Task<AccountViewModel> RemoveById(int id);
    }
}
