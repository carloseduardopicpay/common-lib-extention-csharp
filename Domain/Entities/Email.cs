using System.Text.RegularExpressions;

namespace Domain.Entities
{
    public class Email : MeioContato
    {
        protected Email() { }

        public Email(Dominio tipo, string enderecoEmail)
        {
            this.Tipo = tipo;
            this.Endereco = enderecoEmail.ToLower();
        }

        public virtual string Endereco { get; set; }


        public virtual bool Principal { get; set; }

        #region REGEX VALIDA EMAIL
        public static bool IsValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || email.Contains(".@"))
                return false;

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                              @".)+))([a-zA-Z]{2,}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);

            if (re.IsMatch(email))
                return true;
            else
                return false;
        }
        #endregion REGEX VALIDA EMAIL
    }
}