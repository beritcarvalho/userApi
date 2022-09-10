using System.Text.RegularExpressions;

namespace UserApi.Applications.Dtos.ValueObjects
{
    public class Cpf
    {
        public string Number { get; set; }

        private bool isValid;
        public bool IsValid
        {
            get
            {
                isValid = IsCPFValid(Number);
                return isValid;
            }
            private set { isValid = value; }
        }

        private bool isNumber;
        public bool IsNumber
        {
            get
            {
                isNumber = InputIsNumber(Number);
                return isNumber;
            }
            private set { isNumber = value; }
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

        public bool InputIsNumber(string text)
        {
            var isNumber = !Regex.Match(text, "[^0-9]").Success;
            return isNumber;
        }
    }
}

