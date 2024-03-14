using System;

namespace Domain.Entities
{
    public class DadoEconomicoPessoaJuridica
    {
        protected DadoEconomicoPessoaJuridica() { }

        public DadoEconomicoPessoaJuridica(decimal valorFaturamentoBruto)
        {
            ValorFaturamentoBruto = valorFaturamentoBruto;
        }

        public virtual DateTime? DataBalanco { get; set; }

        public virtual DateTime? DataPosicao { get; set; }

        public virtual DateTime? PeriodoInicial { get; set; }

        public virtual DateTime? PeriodoFinal { get; set; }

        public virtual bool Exporta { get; set; }

        public virtual int Identificacao { get; set; }

        public virtual bool Importa { get; set; }

        public virtual string Observacao { get; set; }

        public virtual Periodicidade Periodicidade { get; set; }

        public virtual decimal ValorCompras { get; set; }

        public virtual decimal ValorFaturamentoBruto { get; set; }

        public virtual decimal ValorFaturamentoLiquido { get; set; }

        public virtual decimal ValorLucroBruto { get; set; }

        public virtual decimal ValorLucroLiquido { get; set; }

        public virtual decimal ValorPatrimonioBruto { get; set; }

        public virtual decimal ValorPatrimonioLiquido { get; set; }

        public virtual decimal ValorReceitaBrutaAnual { get; set; }

        public virtual decimal ValorVendas { get; set; }

        public virtual int QuantidadeFuncionarios { get; set; }

        public virtual Pessoa Pessoa { get; set; }
    }

}