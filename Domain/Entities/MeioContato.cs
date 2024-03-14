namespace Domain.Entities
{
    public abstract class MeioContato
    {
        public virtual int Identificacao { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual Dominio Tipo { get; set; }
        public virtual string Ativo { get; set; }
    }
}