namespace Domain.Entities
{
    public abstract class ClassificacaoTributacao : IDominio
    {
        public virtual int Identificacao { get; set; }

        public virtual string Descricao { get; set; }

        public virtual string Codigo { get; set; }
    }
}