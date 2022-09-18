using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using UserApi.Applications.Dtos.InputModels;

namespace UserApi.Applications.Validators
{
    public class AccountInputModelValidator : AbstractValidator<AccountInputModel>
    {
        public AccountInputModelValidator()
        {
            RuleFor(account => account.Name).SetValidator(new NameValueObjectValidator());

            RuleFor(account => account.Cpf).SetValidator(new CpfValueObjectValidator());

            RuleFor(account => account.Phone).SetValidator(new PhoneValueObjectValidator());

            RuleFor(account => account.Email).SetValidator(new EmailValueObjectValidator());
        }
    }
}


