namespace Domain.Entities
{
    public class EstoqueAgricolaProdutorRural
    {
        public virtual int Identificacao { get; set; }
        public virtual string Produto { get; set; }
        public virtual int AnoSafra { get; set; }
        public virtual int QtdeEstoque { get; set; }
        public virtual decimal ValorEstoque { get; set; }
        public virtual PessoaFisica Pessoa { get; set; }
    }
}