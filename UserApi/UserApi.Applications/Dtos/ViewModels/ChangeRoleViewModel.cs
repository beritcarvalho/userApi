using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class ChangeRoleViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DateTime Last_Update_Date { get; set; }
        public string Role { get; set; }
    }
}
