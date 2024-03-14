namespace Domain.Entities
{
    public class PortePessoaFisica 
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal RendaBrutaMaiorQue { get; protected set; }
        public virtual decimal RendaBrutaMenorIgualA { get; protected set; }
    }
}