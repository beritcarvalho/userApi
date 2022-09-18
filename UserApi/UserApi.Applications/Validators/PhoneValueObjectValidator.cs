using FluentValidation;
using System.Text.RegularExpressions;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class PhoneValueObjectValidator : AbstractValidator<Phone>
    {
        public PhoneValueObjectValidator()
        {
            RuleFor(phone => phone.Ddd)
                .NotNull().WithMessage("O DDD é obrigatório")
                .NotEmpty().WithMessage("O DDD precisa ser informado")
                .DependentRules(() =>
                {
                    RuleFor(phone => phone.Ddd)
                        .Must(ddd => IsNumber(ddd)).WithMessage("O DDD deve conter apenas números")                        
                        .DependentRules(() =>
                        {
                            RuleFor(phone => phone.Ddd)
                                .Must(ddd => ddd.Substring(0, 1) != "0").WithMessage("O DDD não deve iniciar com 0")
                                .Length(2).WithMessage("O DDD deve conter 2 caracteres");
                        });
                });

            RuleFor(phone => phone.Number)
                .NotNull().WithMessage("O Numero do Telefone é obrigatório")
                .NotEmpty().WithMessage("O Numero do Telefone precisa ser informado")
                .DependentRules(() =>
                {
                    RuleFor(phone => phone.Number)
                        .Must(ddd => IsNumber(ddd)).WithMessage("O Numero do Telefone deve conter apenas números")
                        .DependentRules(() =>
                        {
                            RuleFor(phone => phone.Number)
                                .Length(8, 9).WithMessage("O Numero do Telefone conter 8 a 9 caracteres");
                        });
                });
        }


        public bool IsNumber(string text)
        {
            var isValid = !Regex.Match(text, "[^0-9]").Success;
            return isValid;
        }
    }
}