using System;

namespace Domain.Entities
{
    public class Vinculo
    {
        protected Vinculo()
        { }

        public Vinculo(Pessoa pessoa, Pessoa pessoaParticipante)
        {
            this.Pessoa = pessoa;
            this.PessoaParticipante = pessoaParticipante;
        }

        public virtual int Identificacao { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual Pessoa PessoaParticipante { get; set; }

        public virtual Dominio TipoRelacionamento { get; set; }

        public virtual bool IsClienteBCO_JBS { get; set; }

        public virtual Dominio Cargo { get; set; }

        public virtual string Sede { get; set; }

        public virtual decimal PercentualParticipacao { get; set; }

        public virtual decimal PercentualCapitalVotante { get; set; }

        public virtual decimal CapitalSocial
        {
            get
            {
                if (this.Pessoa != null && this.Pessoa is PessoaJuridica)
                {
                    var capitalSocial = (this.Pessoa as PessoaJuridica).CapitalSocial != null ? (this.Pessoa as PessoaJuridica).CapitalSocial.Valor : 0;

                    return (capitalSocial * this.PercentualParticipacao) / 100;
                }

                return 0;
            }
        }

        public virtual decimal FaturamentoFiscal { get; set; }

        public virtual DateTime? DataEntrada { get; set; }

        public virtual DateTime? DataSaida { get; set; }

        public virtual Endereco Endereco { get; set; }

        public virtual Email Email { get; set; }

        public virtual Telefone Telefone { get; set; }

        public virtual bool BenFinal { get; set; }
    }
}