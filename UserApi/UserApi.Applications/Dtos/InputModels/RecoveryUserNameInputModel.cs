using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Dtos.InputModels
{
    public class RecoveryUserNameInputModel
    {
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
    }
}
