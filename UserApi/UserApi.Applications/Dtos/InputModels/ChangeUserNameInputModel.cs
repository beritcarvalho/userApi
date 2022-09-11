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
        public string OldLogin { get; set; }
        public string NewLogin { get; set; }
        public Cpf Cpf { get; set; }
        public string Password_Hash { get; set; }
    }
}
