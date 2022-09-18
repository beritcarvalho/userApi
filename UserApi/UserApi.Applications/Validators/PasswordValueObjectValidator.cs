using FluentValidation;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class PasswordValueObjectValidator : AbstractValidator<PasswordValueObject>
    {
        public PasswordValueObjectValidator()
        {
            RuleFor(passwordInput => passwordInput.Password)
                .NotNull().WithMessage("O campo de senha é requerido")
                .DependentRules(() =>
                    RuleFor(passwordInput => passwordInput.Password)
                        .NotEmpty().WithMessage("Informe a senha"))
                .MinimumLength(4).WithMessage("A senha não pode ter menos que 4 caracteres")
                .MaximumLength(255);
        }
    }
}
