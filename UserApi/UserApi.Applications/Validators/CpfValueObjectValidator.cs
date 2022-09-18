using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UserApi.Applications.Dtos.ValueObjects;

namespace UserApi.Applications.Validators
{
    public class CpfValueObjectValidator : AbstractValidator<Cpf>
    {
        public CpfValueObjectValidator()
        {
            RuleFor(cpf => cpf.Number)
                .NotNull().WithMessage("O CPF é obrigatório")
                .NotEmpty().WithMessage("O CPF precisa ser informado")
                .Length(11).WithMessage("O CPF deve conter 11 caracteres");

            RuleFor(cpf => InputIsNumber(cpf.Number))
                .Must(isnumber => isnumber).WithMessage("O Precisa ser Número");

            RuleFor(cpf => IsCPFValid(cpf.Number))
                .Must(valid => valid).WithMessage("O CPF informado é inválido");
        }

        private bool IsCPFValid(string cpf)
        {
            cpf = Regex.Replace(cpf, "[^0-9]", "");

            if (cpf.Length != 11)
                return false;

            var firstValidator = GetFirstStepValidador(cpf);

            var secondValidator = GetSecondStepValidador(cpf);

            var twoLastNumbersCpf = int.Parse(cpf.Substring(9, 2));

            var validator = int.Parse(string.Concat(firstValidator + secondValidator));

            if (twoLastNumbersCpf == validator)
                return true;

            return false;
        }

        private string GetSecondStepValidador(string cpf)
        {
            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += i * int.Parse(cpf.Substring(i, 1));
            }
            int restDivision = sum % 11;

            if (restDivision == 10)
                restDivision = 0;

            var secondDigitValidator = restDivision.ToString().Substring(0, 1);
            return secondDigitValidator;
        }

        private string GetFirstStepValidador(string cpf)
        {
            int sum = 0;
            for (int i = 1; i < 10; i++)
            {
                sum += i * int.Parse(cpf.Substring(i - 1, 1));
            }

            int restDivision = sum % 11;
            if (restDivision == 10)
                restDivision = 0;

            var firstDigitValidator = restDivision.ToString().Substring(0, 1);

            return firstDigitValidator;
        }

        public bool InputIsNumber(string input)
        {
            var isNumber = !Regex.Match(input, "[^0-9]").Success;
            return isNumber;
        }
    }
}
