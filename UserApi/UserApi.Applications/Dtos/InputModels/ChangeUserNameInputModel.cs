using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Dtos.InputModels
{
    public class ChangeUserNameInputModel
    {
        public UsernameValueObject OldLogin { get; set; }
        public UsernameValueObject NewLogin { get; set; }
        public Cpf Cpf { get; set; }
        public PasswordValueObject PasswordInput { get; set; }
    }
}
