using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Domain.Entities
{
    public class Person : Entity
    {
        public int Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Cpf { get; set; }
        public string Phone { get; set; }
        public string? Email { get; set; }
        public DateTime Create_Date { get; set; }
        public bool Active { get; set; }
        public DateTime? Inactive_Date { get; set; }
        public DateTime? Activation_Date { get; set; }
        public DateTime? Change_Date { get; set; }
    }
}
