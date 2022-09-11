using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class UserActiveViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public bool Active { get; set; }
        public DateTime Active_Date { get; set; }
    }
}
