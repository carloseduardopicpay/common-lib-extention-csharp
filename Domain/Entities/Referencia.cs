namespace Domain.Entities
{
    public abstract class Referencia
    {
        public virtual int Identificacao { get; set; }
        public virtual string Nome { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual string Ativo { get; set; }
    }
}