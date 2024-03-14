using System;

namespace Domain.Entities
{
    public class FonteRenda
    {
        public  int Identificacao { get; set; }

        public  Pessoa ? Pessoa { get; set; }

        public  Funcao FuncaoGeradoraRenda { get; set; }

        public  Dominio TipoFonteRenda { get; set; }

        public  decimal ValorRenda { get; set; }

        public  Dominio Periodicidade { get; set; }

        public  TipoInformacaoRenda TipoInformacao { get; set; }

        public  string Ativo { get; set; }
        /// Projeto Rendas - 21/02/2018
        public  string VinculoOrigem { get; set; }

        public  decimal RendaMensal { get; set; }

        public  Dominio Moeda { get; set; }

        public  string Documento { get; set; }

        public  string NumeroCnpjEmpresa { get; set; }

        public  DateTime? DataDeInclusao { get; set; }

        public  DateTime? DataDeAtualizacao { get; set; }

        public  Banco Banco { get; set; }


        public override bool Equals(object obj)
        {
            return Equals(obj as FonteRenda);
        }

        public  bool Equals(FonteRenda other)
        {
            if (other == null)
                return false;

            bool isSame = false;

            try
            {
                if (this.TipoFonteRenda != null && other.TipoFonteRenda != null)
                {
                    isSame = this.TipoFonteRenda.Identificacao == other.TipoFonteRenda.Identificacao;
                }
                else
                {
                    isSame = this.TipoFonteRenda == null && other.TipoFonteRenda == null;
                }

                if (this.Periodicidade != null && other.Periodicidade != null)
                {
                    isSame &= this.Periodicidade.Identificacao == other.Periodicidade.Identificacao;
                }
                else
                {
                    isSame &= this.Periodicidade == null && other.Periodicidade == null;
                }

                if (this.TipoInformacao != null && other.TipoInformacao != null)
                {
                    isSame &= this.TipoInformacao.Identificacao == other.TipoInformacao.Identificacao;
                }
                else
                {
                    isSame &= this.TipoInformacao == null && other.TipoInformacao == null;
                }

                if (this.Pessoa != null && other.Pessoa != null)
                {
                    isSame &= this.Pessoa.Identificacao == other.Pessoa.Identificacao;
                }
                else
                {
                    isSame &= this.Pessoa == null && other.Pessoa == null;
                }

                if (this.Banco != null && other.Banco != null)
                {
                    isSame &= this.Banco.Identificacao == other.Banco.Identificacao;
                }
                else
                {
                    isSame &= this.Banco == null && other.Banco == null;
                }

                if (this.Moeda != null && other.Moeda != null)
                {
                    isSame &= this.Moeda.Identificacao == other.Moeda.Identificacao;
                }
                else
                {
                    isSame &= this.Moeda == null && other.Moeda == null;
                }
                if (this.DataDeInclusao.HasValue && other.DataDeInclusao.HasValue)
                {
                    isSame = isSame && this.DataDeInclusao.Value.Equals(other.DataDeInclusao.Value);
                }
                if (this.DataDeAtualizacao.HasValue && other.DataDeAtualizacao.HasValue)
                {
                    isSame = isSame && this.DataDeAtualizacao.Value.Equals(other.DataDeAtualizacao.Value);
                }
                isSame &= this.ValorRenda == other.ValorRenda;
                isSame &= this.RendaMensal == other.RendaMensal;
                isSame &= this.VinculoOrigem == other.VinculoOrigem;
                isSame &= this.Documento == other.Documento;
                isSame &= this.NumeroCnpjEmpresa == other.NumeroCnpjEmpresa;

            }
            catch (Exception)
            {
                isSame = base.Equals(other);
            }

            return isSame;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                // Choose large primes to avoid hashing collisions
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Identificacao) ? Identificacao.GetHashCode() : base.GetHashCode());
                return hash;
            }
        }

    }
}