using System.Text.RegularExpressions;

namespace UserApi.Applications.InputModels
{
    public class Cpf
    {
        public bool IsCPFValid(string cpf)
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
            int soma = 0;
            for (int i = 0; i < 10; i++)
            {
                soma += i * int.Parse(cpf.Substring(i, 1));
            }
            int resto = soma % 11;

            if (resto == 10)
                resto = 0;

            var segundoDigito = resto.ToString().Substring(0, 1);
            return segundoDigito;
        }

        private string GetFirstStepValidador(string cpf)
        {
            int soma = 0;
            for (int i = 1; i < 10; i++)
            {
                soma += i * int.Parse(cpf.Substring(i - 1, 1));
            }

            int resto = soma % 11;
            if (resto == 10)
                resto = 0;

            var primeiroDigito = resto.ToString().Substring(0, 1);

            return primeiroDigito;
        }
    }
}
