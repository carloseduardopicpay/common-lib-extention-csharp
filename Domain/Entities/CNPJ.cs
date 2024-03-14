using System.Text.RegularExpressions;
using Volo.Abp;

namespace Domain.Entities
{
    public class CNPJ : IDocumento
    {
        protected CNPJ() { }

        public CNPJ(string numero, Dominio situacao)
            : this(numero)
        {
            this.Situacao = situacao;
        }

        public CNPJ(string numero, string msgErro = "CNPJ inválido")
        {
            if (!CNPJ.IsValid(numero))
                throw new BusinessException(msgErro);

            this.Numero = Regex.Replace(numero, "[^0-9]+", "", RegexOptions.Compiled);
        }

        public static int TamanhoPadrao
        {
            get
            {
                return 14;
            }
        }

        public virtual string Numero
        {
            get;
            protected set;
        }

        public virtual int Sequencia { get; protected set; }

        // Situacao como Dominio
        public virtual Dominio Situacao { get; set; }

        public virtual string Raiz
        {
            get
            {
                return string.IsNullOrEmpty(this.Numero) ? string.Empty : this.Numero.Substring(0, 8);
            }
            protected set
            {
            }
        }

        public override string ToString()
        {
            return this.Numero;
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as CNPJ);
        }

        public virtual bool Equals(CNPJ other)
        {
            if (other == null || string.IsNullOrEmpty(Numero.Trim()))
                return false;

            return Numero.Equals(other.Numero);
        }

        public static bool IsValid(string numeroDocumento)
        {
            if (numeroDocumento == null)
                return false;

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;

            // Despreza qualquer caracter não numérico.
            numeroDocumento = numeroDocumento.Trim();
            numeroDocumento = Regex.Replace(numeroDocumento, "[^0-9]+", "", RegexOptions.Compiled);
            if (numeroDocumento.Length != CNPJ.TamanhoPadrao)
                return false;

            tempCnpj = numeroDocumento.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();
            return numeroDocumento.EndsWith(digito);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static CNPJ NovoDocumentoParaIsento(string numeroSequencial)
        {
            var docIsento = new CNPJ();
            docIsento.Numero = numeroSequencial;
            docIsento.Sequencia = 0;
            return docIsento;
        }

    }

    public interface IDocumento
    {
        string Numero { get; }
        int Sequencia { get; }
        Dominio Situacao { get; set; }
    }
}