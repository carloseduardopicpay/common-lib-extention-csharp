using System;

namespace Domain.Entities
{
    public class Restricao
    {
        protected Restricao()
        { }

        public Restricao(Pessoa pessoa)
        {
            this.Pessoa = pessoa;
            this.DataRegistro = DateTime.Today;
        }

        public virtual int Identificacao { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual DateTime? DataOcorrencia { get; set; }

        public virtual TipoRestricao TipoRestricao { get; set; }

        public virtual Dominio NaturezaRestricao { get; set; }

        public virtual DateTime? DataRegistro { get; set; }

        public virtual string NumeroDocumento { get; set; }

        public virtual FonteConsultaRestricao FonteConsulta { get; set; }

        public virtual string FonteInformacao { get; set; }

        public virtual Dominio DominioFonteInformacao { get; set; }

        public virtual decimal ValorRestricao { get; set; }

        public virtual DateTime? DataRegularizacao { get; set; }

        public virtual DateTime? DataCancelado { get; set; }

        public virtual string Observacao { get; set; }

        public virtual bool Equals(Restricao other)
        {
            if (this == null || other == null)
                return false;

            bool igual = false;

            // A igualdade de restrições se dá quando são iguais: Customer, Data da Ocorrência, Tipo, valor
            try
            {
                igual = (this.Pessoa != null && other.Pessoa != null && this.Pessoa.CodigoExterno == other.Pessoa.CodigoExterno)
                    // Verifica datas iguais
                    && (this.DataOcorrencia.HasValue && other.DataOcorrencia.HasValue && this.DataOcorrencia.Value == other.DataOcorrencia.Value)
                    // Verifica tipos iguais
                    && (this.TipoRestricao != null && other.TipoRestricao != null && this.TipoRestricao.Identificacao == other.TipoRestricao.Identificacao)
                    // Verifica valores iguais
                    && ((this.ValorRestricao == 0 && other.ValorRestricao == 0) || this.ValorRestricao == other.ValorRestricao);
            }
            catch (Exception)
            {
                igual = false;
            }

            return igual;
        }
    }
}