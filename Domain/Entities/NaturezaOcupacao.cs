namespace Domain.Entities
{
    public class NaturezaOcupacao : IDominio
    {
        public  int Identificacao { get; set; }

        public  string Codigo { get; set; }

        public  string Descricao { get; set; }

        public  bool ExigeOcupacaoPrincipal { get; set; }
    }
}