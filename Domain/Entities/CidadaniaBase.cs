namespace Domain.Entities
{
    public abstract class CidadaniaBase
    {
        public virtual int Identificacao { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Pais Pais { get; set; }

        public virtual string DocumentoIdentificacao { get; set; }

        public virtual string Ativo { get; set; }

        public virtual string Tipo { get; set; }
    }
}