﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.InputModels;

namespace UserApi.Applications.Validators
{
    public class RecoveryUserNameInputModelValidator : AbstractValidator<RecoveryUserNameInputModel>
    {
        public RecoveryUserNameInputModelValidator()
        {
            RuleFor(input => input.Cpf).SetValidator(new CpfValueObjectValidator());

            RuleFor(input => input.Phone).SetValidator(new PhoneValueObjectValidator());
        }
    }
}
