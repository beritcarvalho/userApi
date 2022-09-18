using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Dtos.InputModels
{
    public class AccountInputModel
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public Cpf Cpf { get; set; }
        public Phone Phone { get; set; }
        public Email Email { get; set; }
    }
}
