using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;

namespace UserApi.Applications.Validators
{
    public class UserInputModelValidator : AbstractValidator<UserInputModel>
    {
        public UserInputModelValidator()
        {
            RuleFor(user => user.Login).SetValidator(new UsernameValueObjectValidator());

            RuleFor(user => user.PropPassword).SetValidator(new PasswordValueObjectValidator());

            RuleFor(user => user.Account_Id).NotEqual(0);

            RuleFor(user => user.Role_Id).NotEqual(0);
        }
    }
}
