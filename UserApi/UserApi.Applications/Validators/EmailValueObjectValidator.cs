using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class EmailValueObjectValidator : AbstractValidator<Email>
    {
        public EmailValueObjectValidator()
        {
            RuleFor(email => email.EmailAddress)
                .NotNull().WithMessage("O campo Email é requerido")
                .DependentRules(() =>
                    RuleFor(email => email.EmailAddress)
                        .NotEmpty().WithMessage("O Email precisa ser informado"))
                .MinimumLength(6);

            RuleFor(email => email.EmailAddress).EmailAddress();
        }
    }
}
