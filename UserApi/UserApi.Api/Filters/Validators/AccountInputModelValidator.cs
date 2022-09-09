using FluentValidation;
using System.Text.RegularExpressions;
using UserApi.Applications.InputModels;

namespace UserApi.Api.Filters.Validators
{
    public class AccountInputModelValidator : AbstractValidator<AccountInputModel>
    {
        public AccountInputModelValidator()
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
            
            RuleFor(account => account.Cpf)
                .NotNull().WithMessage("O CPF é obrigatório")
                .NotEmpty().WithMessage("O CPF precisa ser informado")
                .DependentRules(() =>
                {
                    RuleFor(account => account.Cpf)
                        .Length(11).WithMessage("O CPF deve conter 11 caracteres")
                        .Must(cpf => InputIsNumber(cpf)).WithMessage("O CPF deve conter apenas numeros");
                });

            RuleFor(account => account.Phone.Ddd)
                .NotNull().WithMessage("O DDD é obrigatório")
                .NotEmpty().WithMessage("O DDD precisa ser informado")
                .DependentRules(() =>
                {
                    RuleFor(account => account.Phone.Ddd)
                        .Must(ddd => InputIsNumber(ddd)).WithMessage("O DDD deve conter apenas números")
                        .DependentRules(() =>
                        {
                            RuleFor(account => account.Phone.Ddd)
                                .Length(2).WithMessage("O DDD deve conter 2 caracteres");
                        });
                });

            RuleFor(account => account.Phone.Number)
                .NotNull().WithMessage("O Numero do Telefone é obrigatório")
                .NotEmpty().WithMessage("O Numero do Telefone precisa ser informado")
                .DependentRules(() =>
                {
                    RuleFor(account => account.Phone.Number)
                        .Must(ddd => InputIsNumber(ddd)).WithMessage("O Numero do Telefone deve conter apenas números")
                        .DependentRules(() =>
                        {
                            RuleFor(account => account.Phone.Number)
                                .Length(8, 9).WithMessage("O Numero do Telefone entre 8 a 9 caracteres");
                        });
                });

            RuleFor(account => account.Email).EmailAddress();
                
        }
        

        private bool InputIsNumber (string text)
        {
            var isValid = !Regex.Match(text, "[^0-9]").Success;
            return isValid;
        }        
    } 
}


