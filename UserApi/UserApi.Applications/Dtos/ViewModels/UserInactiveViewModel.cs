using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class UserInactiveViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool Active { get; set; }
        public DateTime Inactive_Date { get; set; }
    }
}
