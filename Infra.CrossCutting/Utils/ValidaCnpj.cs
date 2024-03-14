using System.Text.RegularExpressions;

namespace Infra.CrossCutting.Utils
{
    public class ValidaCNPJ
    {
        public static bool ValidarCNPJ(string cnpj)
        {
            // Remover caracteres não numéricos
            cnpj = Regex.Replace(cnpj, "[^0-9]", "");

            // Verificar se o CNPJ possui 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Calcular os dígitos verificadores
            int[] multiplicadoresPrimeiroDigito = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadoresSegundoDigito = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string cnpjSemDigitos = cnpj.Substring(0, 12);
            int primeiroDigitoVerificador = CalcularDigitoVerificador(cnpjSemDigitos, multiplicadoresPrimeiroDigito);
            int segundoDigitoVerificador = CalcularDigitoVerificador(cnpjSemDigitos + primeiroDigitoVerificador, multiplicadoresSegundoDigito);

            // Verificar se os dígitos verificadores calculados são iguais aos dígitos verificadores informados
            return cnpj.EndsWith(primeiroDigitoVerificador.ToString() + segundoDigitoVerificador.ToString());
        }

        private static int CalcularDigitoVerificador(string input, int[] multiplicadores)
        {
            int soma = 0;

            for (int i = 0; i < input.Length; i++)
            {
                soma += int.Parse(input[i].ToString()) * multiplicadores[i];
            }

            int resto = soma % 11;
            return resto < 2 ? 0 : 11 - resto;
        }
    }
}
