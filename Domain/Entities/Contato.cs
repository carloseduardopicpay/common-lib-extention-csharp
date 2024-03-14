using System.Collections.Generic;

namespace Domain.Entities
{
    public class Contato
    {
        public Contato()
        {
            this.Telefones = new List<ContatoTelefone>();
            this.Emails = new List<ContatoEmail>();
        }

        public virtual int Identificacao { get; set; }

        public virtual Dominio Tipo { get; set; }

        public virtual string Nome { get; set; }

        public virtual string CPF { get; set; }

        public virtual string Departamento { get; set; }

        public List<ContatoTelefone> Telefones { get; set; }

        public List<ContatoEmail> Emails { get; set; }

        public virtual string WebSite { get; set; }

        public virtual string Funcao { get; set; }

        public virtual string Observacao { get; set; }
    }
}
