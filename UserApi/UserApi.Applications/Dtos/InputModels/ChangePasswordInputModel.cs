using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Dtos.InputModels
{
    public class ChangePasswordInputModel
    {
        public UsernameValueObject Login { get; set; }
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
        public PasswordValueObject Old_Password { get; set; }
        public PasswordValueObject New_Password { get; set; }
    }
}
