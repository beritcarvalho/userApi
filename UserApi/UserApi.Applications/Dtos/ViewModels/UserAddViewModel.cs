using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class UserAddViewModel
    {
        public int Id { get; set; }
        public Name Name { get; set; }
        public string Login { get; set; }
        public string Role { get; set; }
        public DateTime Create_Date { get; set; }
        public bool Active { get; set; }        
    }
}
