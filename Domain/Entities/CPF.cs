using System.Text.RegularExpressions;
using System;
using Volo.Abp;

namespace Domain.Entities
{
    public class CPF : IDocumento
    {
        protected CPF() { }

        public CPF(string numero, int sequencia = 0, string msgErro = "Número de CPF inválido")
        {
            if (string.IsNullOrEmpty(numero))
            {
                this.Numero = null;
                return;
            }

            if (!CPF.IsValid(numero))
                throw new BusinessException(msgErro);

            this.Numero = Regex.Replace(numero, "[^0-9]+", "", RegexOptions.Compiled);
            Sequencia = sequencia;
        }

        public static int TamanhoPadrao
        {
            get
            {
                return 11;
            }
        }

        public virtual string Numero { get; protected set; }

        public virtual Dominio Situacao { get; set; }

        public virtual int Sequencia { get; protected set; }

        public override string ToString()
        {
            return this.Numero;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CPF);
        }

        public virtual bool Equals(CPF other)
        {
            if (other == null || string.IsNullOrEmpty(Numero.Trim()))
                return false;

            return Numero.Equals(other.Numero) && Sequencia.Equals(other.Sequencia);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool IsValid(string numeroDocumento)
        {
            if (numeroDocumento == null)
                return false;

            //int multiplicador1 = 10;
            //int multiplicador2 = 11;

            if (numeroDocumento == "00000000000" ||
                numeroDocumento == "11111111111" ||
                numeroDocumento == "22222222222" ||
                numeroDocumento == "33333333333" ||
                numeroDocumento == "44444444444" ||
                numeroDocumento == "55555555555" ||
                numeroDocumento == "66666666666" ||
                numeroDocumento == "77777777777" ||
                numeroDocumento == "88888888888" ||
                numeroDocumento == "99999999999")
            {
                return false;
            }
            else
            {
                string tempCpf;

                numeroDocumento = numeroDocumento.Trim().Replace(".", "").Replace("-", "");
                if (numeroDocumento.Length != CPF.TamanhoPadrao)
                    return false;

                tempCpf = numeroDocumento.Substring(0, 9);

                // Calcula primeiro digito verificador.
                tempCpf += CalcularDigitoVerificador(tempCpf);
                // Calcula segundo digito verificador.
                tempCpf += CalcularDigitoVerificador(tempCpf);

                return numeroDocumento.Equals(tempCpf);
            }
        }

        public static string CalcularDigitoVerificador(string numeroDocumento)
        {
            // Cálculo do módulo 11.
            // DV = Digito verificador.

            // Somatória do resultado.
            int soma = 0;

            try
            {
                // Passa número a número da chave pegando da direita pra esquerda (pra isso o Reverse()).
                for (int i = 0; i < numeroDocumento.Length; i++)
                    soma += int.Parse(numeroDocumento[i].ToString()) * (numeroDocumento.Length + 1 - i);

                // DV = 11 - (resto da divisão)
                // Quando o resto da divisão for 0 (zero) ou 1 (um), o DV deverá ser igual a 0 (zero).
                int resto = soma % 11;

                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;

                return resto.ToString();
            }
            catch
            {
                throw new ArgumentException("ERRO: A chave de acesso deve conter apenas números.");
            }

        }

        public static CPF NovoDocumentoParaIsento(string numeroSequencial)
        {
            var docIsento = new CPF();
            docIsento.Numero = numeroSequencial;
            docIsento.Sequencia = 0;
            return docIsento;
        }
    }
}