using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.InputModels
{
    public class UserInputModel
    {
        public int Id { get; set; }
        public UsernameValueObject Login { get; set; }
        public PasswordValueObject PropPassword { get; set; }
        public bool Active { get; set; }
        public int Account_Id { get; set; }
        public int Role_Id { get; set; }
    }
}
