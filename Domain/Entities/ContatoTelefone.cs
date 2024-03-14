namespace Domain.Entities
{
    public class ContatoTelefone : Telefone
    {
        protected ContatoTelefone() { }

        public ContatoTelefone(Contato contato, Dominio tipo, string DDI, string DDD, string numero, string ramal)
            : base(tipo, DDI, DDD, numero, ramal)
        {
            this.Contato = contato;
        }

        public ContatoTelefone(Contato contato, Dominio tipoTelefone, string DDD, string numero, string ramal)
            : base(tipoTelefone, DDD, numero, ramal)
        {
            this.Contato = contato;
        }

        public ContatoTelefone(Contato contato, Dominio tipoTelefone, string numero, string ramal)
            : base(tipoTelefone, string.Empty, numero, ramal)
        {
            this.Contato = contato;
        }

        public ContatoTelefone(Contato contato, Dominio tipoTelefone, string numero)
            : base(tipoTelefone, numero)
        {
            this.Contato = contato;
        }

        public virtual Contato Contato { get; set; }
    }
}