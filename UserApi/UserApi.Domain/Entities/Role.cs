using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Domain.Entities
{
    [Table("Role", Schema = "AccessControl")]
    public class Role : Entity
    {
        [Key]
        [Column("Role_Id", TypeName = "INT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Name", TypeName = "NVARCHAR")]
        [MinLength(2, ErrorMessage = "A quantidade de caractere não pode ser menor que 2")]
        [MaxLength(8, ErrorMessage = "A quantidade de caractere não pode ser maior que 8")]
        public string Name { get; set; }       
    }
}
