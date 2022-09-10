using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class CpfValidator : AbstractValidator<Cpf>
    {
        public CpfValidator()
        {
            RuleFor(cpf => cpf.Number)
                .NotNull().WithMessage("O CPF é obrigatório")
                .NotEmpty().WithMessage("O CPF precisa ser informado")
                .Length(11).WithMessage("O CPF deve conter 11 caracteres");

            RuleFor(cpf => cpf.IsNumber)
                .Must(isnumber => isnumber).WithMessage("O Precisa ser Número");

            RuleFor(cpf => cpf.IsValid)
                .Must(valid => valid).WithMessage("O CPF informado é inválido");
        }
    }
}
