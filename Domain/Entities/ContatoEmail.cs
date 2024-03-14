namespace Domain.Entities
{
    public class ContatoEmail : Email
    {
        protected ContatoEmail() { }
        public ContatoEmail(Contato contato, Dominio tipo, string endereco) 
        {
            this.Contato = contato;
        }

        public virtual Contato Contato { get; set; }
    }
}