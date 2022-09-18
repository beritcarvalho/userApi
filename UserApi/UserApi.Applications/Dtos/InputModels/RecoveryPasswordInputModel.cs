using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.InputModels
{
    public class RecoveryPasswordInputModel
    {
        public UsernameValueObject Login { get; set; }
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
    }
}
