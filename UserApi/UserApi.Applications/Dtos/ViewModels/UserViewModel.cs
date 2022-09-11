using UserApi.Applications.Dtos.ValueObjects;
using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Create_Date { get; set; }
        public bool Active { get; set; }
        public DateTime? Active_Date { get; set; }
        public DateTime? Inactive_Date { get; set; }
        public DateTime Last_Update_Date { get; set; }
        public string Role { get; set; }
    }
}
