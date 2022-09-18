using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class NameValueObjectValidator : AbstractValidator<Name>
    {
        public NameValueObjectValidator()
        {
            RuleFor(account => account.First_Name)
                            .NotNull().WithMessage("O campo nome é requerido")
                            .DependentRules(() =>
                                RuleFor(account => account.First_Name)
                                    .NotEmpty().WithMessage("O nome precisa ser informado"))
                            .MinimumLength(2).WithMessage("O nome não pode ter menos que 2 caracteres")
                            .MaximumLength(30).WithMessage("O nome não pode ter mais que 30 caracteres");

            RuleFor(account => account.Last_Name)
                .NotNull().WithMessage("O campo sobrenome é requerido")
                .DependentRules(() =>
                    RuleFor(account => account.First_Name)
                        .NotEmpty().WithMessage("O sobrenome precisa ser informado")
                .MinimumLength(2).WithMessage("O sobrenome não pode ter menos que 2 caracteres")
                .MaximumLength(30).WithMessage("O sobrenome não pode ter mais que 30 caracteres"));
        }
    }
}
