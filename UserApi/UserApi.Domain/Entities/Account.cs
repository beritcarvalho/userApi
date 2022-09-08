using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Domain.Entities
{
    public class Account : Entity
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public DateTime Create_Date { get; set; }
        public DateTime? Last_Update_Date { get; set; }

        public User User { get; set; }

    }
}
