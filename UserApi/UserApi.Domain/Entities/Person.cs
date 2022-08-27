using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Domain.Entities
{
    [Table("Person", Schema = "AccessControl")]
    public class Person : Entity
    {
        [Key]
        [Column("Pers_Id", TypeName = "INT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("First_Name", TypeName = "NVARCHAR")]
        [MinLength(2, ErrorMessage = "A quantidade de caractere não pode ser menor que 2")]
        [MaxLength(30, ErrorMessage = "A quantidade de caractere não pode ser maior que 30")]
        public string First_Name { get; set; }

        [Required]
        [Column("Last_Name", TypeName = "NVARCHAR")]
        [MinLength(2, ErrorMessage = "A quantidade de caractere não pode ser menor que 2")]
        [MaxLength(30, ErrorMessage = "A quantidade de caractere não pode ser maior que 30")]
        public string Last_Name { get; set; }

        [Required]
        [Column("Cpf", TypeName = "VARCHAR")]
        [MinLength(11, ErrorMessage = "CPF Inválido, quantidade precisa ser igual a 11")]
        [MaxLength(11, ErrorMessage = "CPF Inválido, quantidade precisa ser igual a 11")]
        public string Cpf { get; set; }

        [Required]
        [Column("Phone", TypeName = "VARCHAR")]
        [MinLength(8, ErrorMessage = "A quantidade de caractere mínima é 8")]
        [MaxLength(11, ErrorMessage = "A quantidade de caractere máxima é 11")]
        public string Phone { get; set; }

        [Column("Email", TypeName = "VARCHAR")]
        [MinLength(6, ErrorMessage = "A quantidade de caractere mínima é 6")]
        [MaxLength(50, ErrorMessage = "A quantidade de caractere máxima é 50")]
        public string Email { get; set; }

        [Required]
        [Column("Create_Date", TypeName = "DATETIME")]
        public DateTime Create_Date { get; set; }

        [Required]
        [Column("Active", TypeName = "BIT")]
        public bool Active { get; set; }

        [Column("Inactive_Date", TypeName = "DATETIME")]
        public DateTime? Inactive_Date { get; set; }
    }
}
