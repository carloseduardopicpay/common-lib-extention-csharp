namespace Domain.Entities
{
    public class FaixaInvestimento 
    {
        public int Identificacao { get; set; }
        public string? Descricao { get; set; }
        public float FaixaInicial { get; set; }
        public float FaixaFinal { get; set; }
    }
}