using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.InputModels;
using UserApi.Applications.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IAccountService
    {
        Task<AccountViewModel> GetById(int id);
        Task<AccountViewModel> AddAccount(AccountInputModel account);
    }
}
