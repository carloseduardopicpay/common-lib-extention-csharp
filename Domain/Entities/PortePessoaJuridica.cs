namespace Domain.Entities
{
    public class PortePessoaJuridica 
    {
        public  int Identificacao { get; set; }
        public  string ? Descricao { get; set; }
        public  decimal ValorReceitaBrutaMaiorQue { get; protected set; }
        public  decimal ValorReceitaBrutaMenorIgual { get; protected set; }
    }
}