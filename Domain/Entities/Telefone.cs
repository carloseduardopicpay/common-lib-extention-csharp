using System.Linq;
using Volo.Abp;

namespace Domain.Entities
{
    public class Telefone : MeioContato
    {
        private string _Numero;

        protected Telefone() { }

        public Telefone(Dominio tipo, string DDI, string DDD, string numero, string ramal)
        {
            this.DDI = !string.IsNullOrWhiteSpace(DDI) ? DDI : "55";
            this.DDD = DDD ?? string.Empty;
            this.Numero = numero ?? string.Empty;
            this.Ramal = ramal ?? string.Empty;

            this.Tipo = tipo;
            this.DDI = this.DDI.Trim();
            this.DDD = this.DDD.Trim();
            this.Ramal = this.Ramal.Trim();

            if ((tipo != null && tipo.Descricao != "DDG") && DDD == "")
            {
                throw new BusinessException("DDD obrigatório para o tipo de telefone!");
            }
        }

        public Telefone(Dominio tipoTelefone, string DDD, string numero, string ramal)
            : this(tipoTelefone, string.Empty, DDD, numero, ramal)
        { }

        public Telefone(Dominio tipoTelefone, string DDD, string numero)
            : this(tipoTelefone, string.Empty, DDD, numero, string.Empty)
        { }

        /// <summary>
        /// Este construtor tenta extrair o DDD do número do telefone. 
        /// </summary>
        /// <param name="tipoTelefone"></param>
        /// <param name="numero">Número do telefone com o DDD </param>
        public Telefone(Dominio tipoTelefone, string numero)
        {
            this.Tipo = tipoTelefone;
            this.DDI = "55";
            this.DDD = string.Empty;
            this.Numero = numero ?? string.Empty;
            this.Ramal = string.Empty;

            if (string.IsNullOrWhiteSpace(numero))
                return;

            int inicioDDD = 0;
            string numeroTel = numero.Trim();
            string dddTel = numeroTel;

            string raizTel = numeroTel.Length > 4 ? numeroTel.Substring(0, 4) : numeroTel;

            if ((tipoTelefone != null && tipoTelefone.Descricao == "DDG") || (raizTel.Equals("0800") || raizTel.Equals("0300")))
                return;

            // Então se o primeiro digito for 0, despreza-o.
            if (dddTel[0].Equals('0'))
                inicioDDD += 1;

            // Os dois primeiros digitos devem ser o DDD.
            dddTel = dddTel.Substring(inicioDDD, 2);
            // Os demais são o número do telefone.
            numeroTel = numeroTel.Substring(inicioDDD + 2);

            this.DDD = dddTel;
            this.Numero = numeroTel;
        }

        public virtual string DDI { get; set; }

        public virtual string DDD { get; set; }

        //private string _ddd;
        //public virtual string DDD 
        //{ 
        //    get
        //    {
        //        return _ddd;
        //    }
        //    set
        //    {
        //        int dddValido;
        //        if (int.TryParse(value, out dddValido) && dddValido > 0 && dddValido < 11)
        //            throw new BusinessException("DDD inválido.");

        //        _ddd = value;
        //    }
        //}

        public virtual string Numero
        {
            get { return _Numero; }

            set
            {
                _Numero = new string(value.Where(char.IsNumber).ToArray());
            }
        }

        public virtual string Ramal { get; set; }
        public Dominio Tipo { get; }
        public virtual Endereco Endereco { get; set; }

        public virtual bool Principal { get; set; }
    }
}