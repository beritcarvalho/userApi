using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.InputModels
{
    public class ForgetInputModel
    {
        public string Login { get; set; }
        public string Password_Hash { get; set; }
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
    }
}
