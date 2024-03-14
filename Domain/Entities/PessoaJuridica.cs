using System.Collections.Generic;
using System.Linq;
using System;

namespace Domain.Entities
{
    public class PessoaJuridica : Pessoa//, ICloneable
    {
        internal PessoaJuridica()
            : base()
        {
            this.SetoresDeAtividades = new List<SetorPessoa>();
            this.Nome = new NomePessoaJuridica(string.Empty);
        }

        public PessoaJuridica(CNPJ cnpj)
            : this()
        {
            this.CNPJ = cnpj;
        }

        public PessoaJuridica(string cnpj, bool isentoCNPJ = false)
            : this()
        {
            if (!isentoCNPJ)
                CNPJ = new CNPJ(cnpj);
            else
                CNPJ = CNPJ.NovoDocumentoParaIsento(cnpj);
        }

        public PessoaJuridica(string cnpj, string razaoSocial, bool isentoCNPJ = false, string codigoExterno = null)
            : this(cnpj, isentoCNPJ)
        {
            Nome = new NomePessoaJuridica(razaoSocial);

            if (!string.IsNullOrEmpty(codigoExterno))
                this.CodigoExterno = codigoExterno;

            this.DataRenovacao = DateTime.Today;
        }

        public PessoaJuridica(CNPJ cnpj, NomePessoaJuridica Nome)
            : this(cnpj)
        {
            this.Nome = Nome;
        }

        public new  NomePessoaJuridica Nome
        {
            get
            {
                return base.Nome as NomePessoaJuridica;
            }
            set
            {
                base.Nome = value;
            }
        }

        public  CNPJ CNPJ { get; set; }

        public  string NomeFantasia { get; set; }

        public  DateTime? DataFundacao { get; set; }

        public  Pais Nacionalidade { get; set; }

        public  Municipio Municipio { get; set; }

        public  PortePessoaJuridica Porte { get; set; }

        public  CapitalSocial CapitalSocial { get; set; }

        public  IList<SetorPessoa> SetoresDeAtividades { get; set; }

        public  Dominio FormaConstituicao { get; set; }

        public  Dominio NaturezaEmpresa { get; set; }

        public  string NumeroContrato { get; set; }

        //public  TipoInstituicao TipoInstituicao { get; set; }

        public  Dominio TipoControleBACEN { get; set; }

        public  TipoControleAcionario ControleAcionario { get; set; }

        public  Pais PaisControleAcionarioEstrangeiro { get; set; }

        public  bool ExibirRelatorioPendencias { get; set; }

        public  bool Matriz { get; set; }

        public  DateTime? DataUltAlterContratual { get; set; }

        public  Dominio MotivoAlteracaoContrato { get; set; }

        public  string Cetip { get; set; }

        public  string Selic { get; set; }

        public  string ISPB { get; set; }

        public  string Compensacao { get; set; }

        public  string InscricaoEstadual { get; set; }

        public  TipoRegimeTributacao TipoRegimeTributacao { get; set; }

        public  Dominio PorteReceita { get; set; }

        public  bool Simei { get; set; }

        public  decimal GetValorFaturamentoBruto()
        {
            decimal renda = 0;

            if (this.DadosEconomicos != null && this.DadosEconomicos.Count > 0)
            {
                var dadoEconomico = this.DadosEconomicos.OrderByDescending(x => x.PeriodoFinal).FirstOrDefault();
                renda = dadoEconomico.ValorFaturamentoBruto;
            }

            return renda;
        }

        public override int GetHashCode()
        {
            if (this.Identificacao != 0)
                return this.Identificacao;
            else
                return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PessoaJuridica);
        }

        public  bool Equals(PessoaJuridica other)
        {
            if (other == null)
                return false;

            if (this.CNPJ == null || other.CNPJ == null)
                return ReferenceEquals(this, other);

            bool isSame = this.CNPJ.Equals(other.CNPJ);

            if (this.Identificacao != 0 && other.Identificacao != 0)
            {
                isSame = isSame && this.Identificacao.Equals(other.Identificacao);
            }

            return isSame;
        }

        //public  object Clone()
        //{
        //    //return this.DeepClone();  -- chama NHIbernete
        //}
    }
}