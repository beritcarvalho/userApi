using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserApi.Domain.Entities
{
    [Table("User", Schema = "AccessControl")]
    public class User : Entity
    {
        [Key]
        [Column("User_Id", TypeName = "INT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("FK_User_Account")]
        [Column("Account", TypeName = "INT")]
        [Required]
        public int AccountId { get; set; }
        public Account Account { get; set; }


        [Column("Login", TypeName = "NVARCHAR")]
        [Required]
        [MinLength(4)]
        [MaxLength(15)]
        public string Login { get; set; }

        [ForeignKey("FK_User_Role")]
        [Column("Role", TypeName = "INT")]
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Column("Password_Hash", TypeName = "VARCHAR")]
        [Required]
        [MinLength(1, ErrorMessage = "A senha precisa ter minimamente 1 caracter")]
        [MaxLength(255)]
        public string Password_Hash { get; set; }

        [Column("Create_Date", TypeName = "DATETIME")]
        [Required]
        public DateTime Create_Date { get; set; }

        [Column("Active", TypeName = "BIT")]
        [Required]
        public bool Active { get; set; }


        [Column("Inactive_Date", TypeName = "DATETIME")]
        public DateTime? Inactive_Date { get; set; }
    }
}
