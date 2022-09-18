using FluentValidation;
using UserApi.Applications.Dtos.InputModels;

namespace UserApi.Applications.Validators
{
    public class ChangePasswordInputModelValidator : AbstractValidator<ChangePasswordInputModel>
    {
        public ChangePasswordInputModelValidator()
        {
            RuleFor(input => input.Login).SetValidator(new UsernameValueObjectValidator());

            RuleFor(input => input.Cpf).SetValidator(new CpfValueObjectValidator());

            RuleFor(input => input.Phone).SetValidator(new PhoneValueObjectValidator());

            RuleFor(input => input.New_Password).SetValidator(new PasswordValueObjectValidator());

            RuleFor(input => input.Old_Password).SetValidator(new PasswordValueObjectValidator());
        }
    }
}
