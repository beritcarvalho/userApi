using UserApi.Domain.Entities;

namespace UserApi.Applications.Dtos.InputModels
{
    public class UpdateUserInputModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password_Hash { get; set; }
        public DateTime Create_Date { get; set; }
        public bool Active { get; set; }
        public DateTime? Active_Date { get; set; }
        public DateTime? Inactive_Date { get; set; }
        public DateTime Last_Update_Date { get; set; }

        public int Acco_Id { get; set; }
        public Account Account { get; set; }

        public int Role_Id { get; set; }
        public Role Role { get; set; }
    }
}
