namespace Domain.Entities
{
    public class UnidadeProdutorRural
    {
        public virtual int Identificacao { get; set; }
        public virtual string Unidade { get; set; }
        public virtual PessoaFisica Pessoa { get; set; }
    }
}