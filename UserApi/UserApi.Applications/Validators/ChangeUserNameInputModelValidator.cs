using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;

namespace UserApi.Applications.Validators
{
    public class ChangeUserNameInputModelValidator : AbstractValidator<ChangeUserNameInputModel>
    {
        public ChangeUserNameInputModelValidator()
        {
            RuleFor(input => input.OldLogin).SetValidator(new UsernameValueObjectValidator());

            RuleFor(input => input.NewLogin).SetValidator(new UsernameValueObjectValidator());

            RuleFor(input => input.Cpf).SetValidator(new CpfValueObjectValidator());

            RuleFor(input => input.PasswordInput).SetValidator(new PasswordValueObjectValidator());
        }
    }
}
