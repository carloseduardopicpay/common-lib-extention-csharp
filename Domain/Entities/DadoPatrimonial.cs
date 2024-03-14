using System;

namespace Domain.Entities
{
    public class DadoPatrimonial
    {
        protected  int Identificacao { get; set; }
        public  Dominio ? TipoImovelResidencial { get; set; }
        public  Dominio? CondicaoImovelResidencial { get; set; }
        public  int AnoInicioResidencia { get; set; }
        public  DateTime? ResideDesde { get; set; }
        public  bool ImovelResidencialFinanciado { get; set; }
        public  decimal ValorImovelResidencial { get; set; }
        public  int QuantidadeImoveisUrbanos { get; set; }
        public  decimal ValorImoveisUrbanos { get; set; }
        public  int QuantidadeImoveisRurais { get; set; }
        public  decimal ValorImoveisRurais { get; set; }
        public  int QuantidadeVeiculos { get; set; }
        public  decimal ValorVeiculos { get; set; }
        public  decimal ValorInvestimento { get; set; }
        public  decimal ValorPatrimonioTotal { get; set; }
        public  FaixaInvestimento ? FaixaValorInvestimentos { get; set; }
    }
}