namespace Domain.Entities
{
    public class SetorPessoa
    {
        public virtual int Identificacao { get; set; }

        public virtual SetorAtividade CNAE { get; set; }

        public virtual Pessoa? Pessoa { get; set; }

        public virtual bool Principal { get; set; }

        public virtual string? Ativo { get; set; }
    }
}