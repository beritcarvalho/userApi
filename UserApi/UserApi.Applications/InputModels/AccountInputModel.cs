using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApi.Applications.InputModels
{
    public class AccountInputModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(2, ErrorMessage = "A quantidade de caractere não pode ser menor que 2")]
        [MaxLength(30, ErrorMessage = "A quantidade de caractere não pode ser maior que 30")]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "O sobrenome é obrigatório")]
        [MinLength(2, ErrorMessage = "A quantidade de caractere não pode ser menor que 2")]
        [MaxLength(30, ErrorMessage = "A quantidade de caractere não pode ser maior que 30")]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        [MinLength(11, ErrorMessage = "CPF Inválido, quantidade precisa ser igual a 11")]
        [MaxLength(11, ErrorMessage = "CPF Inválido, quantidade precisa ser igual a 11")]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "Informe o telefone para contato")]
        [MinLength(8, ErrorMessage = "A quantidade de caractere mínima é 8")]
        [MaxLength(11, ErrorMessage = "A quantidade de caractere máxima é 11")]
        public string Phone { get; set; }

        [MinLength(6, ErrorMessage = "A quantidade de caractere mínima é 6")]
        [MaxLength(50, ErrorMessage = "A quantidade de caractere máxima é 50")]
        public string? Email { get; set; }
    }
}
