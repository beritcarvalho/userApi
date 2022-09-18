using FluentValidation;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class UsernameValueObjectValidator : AbstractValidator<UsernameValueObject>
    {
        public UsernameValueObjectValidator()
        {
            RuleFor(username => username.Username)
                .NotNull().WithMessage("O campo de Login é requerido")
                .DependentRules(() =>
                    RuleFor(passwordInput => passwordInput.Username)
                        .NotEmpty().WithMessage("Informe o login"))
                .MinimumLength(4).WithMessage("O login não pode ter menos que 4 caracteres")
                .MaximumLength(30);
        }
    }
}
