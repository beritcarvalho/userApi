using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;
using UserApi.Applications.Dtos.ViewModels;

namespace UserApi.Applications.Interfaces
{
    public interface IRecoveryService
    {
        Task<RecoveryPasswordViewModel> ForgetPassword(RecoveryPasswordInputModel input);
        Task<ChangePasswordViewModel> ChangePassword(ChangePasswordInputModel input);
        Task<RecoveryUsernameViewModel> GetUserName(RecoveryUserNameInputModel input);
        Task<ChangeUserNameViewModel> ChangeUserName(ChangeUserNameInputModel input);
    }
}
