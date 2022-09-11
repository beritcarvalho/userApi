using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.InputModels
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool Success { get; set; }
    }
}
