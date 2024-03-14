using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    public
     class PessoaFisica : Pessoa, IEquatable<PessoaFisica>
    {
        internal PessoaFisica() : base()
        {
            Nome = new NomePessoaFisica(string.Empty);
            this.Cidadanias = new List<CidadaniaBase>();
            this.Dependentes = new List<Dependente>();
            this.DocumentosIdentificacao = new List<DocumentoIdentificacao>();
            this.PessoaExpostaPoliticamente = new List<PEP>();
            this.FontesRenda = new List<FonteRenda>();
            this.MeiosDeContato = new List<MeioContato>();
            this.DadosProfissionais = new DadoProfissional();
        }

        public
         PessoaFisica(string cpf, string nome, bool isentoCPF = false,
                      string codigoExterno = null, string origemCadastro = null)
             : this()
        {
            if (!isentoCPF)
                this.CPF = new CPF(cpf, 0);
            else
                this.CPF = CPF.NovoDocumentoParaIsento(cpf);

            this.Nome = new NomePessoaFisica(nome);

            if (!string.IsNullOrEmpty(codigoExterno))
                this.CodigoExterno = codigoExterno;

            if (!string.IsNullOrEmpty(origemCadastro)) this.Origem = origemCadastro;

            this.DataRenovacao = DateTime.Today;
        }

        public
         PessoaFisica(CPF cpf, NomePessoaFisica nome) : this()
        {
            this.Nome = nome;
            this.CPF = cpf;
        }

        public
         PessoaFisica(CPF cpf, string nome) : this()
        {
            this.Nome = new NomePessoaFisica(nome);
            this.CPF = cpf;
        }

        public
         PessoaFisica(CPF cpf, Dominio prefixo, string nomePrimeiro, string nomeMeio,
                      string nomeSobrennome, Dominio sufixo)
             : this()
        {
            this.Nome = new NomePessoaFisica(prefixo, nomePrimeiro, nomeMeio,
                                             nomeSobrennome, sufixo);
            this.CPF = cpf;
        }

        public
         CPF CPF
        {
            get;
            protected
             set;
        }

        public
         bool IsencaoCpf
        {
            get;
            set;
        }

        public
         new NomePessoaFisica Nome
        {
            get { return base.Nome as NomePessoaFisica; }
            set { base.Nome = value; }
        }

        public
         string Codinome
        {
            get;
            set;
        }

        public
         Dominio Sexo
        {
            get;
            set;
        }

        public
         Dominio EstadoCivil
        {
            get;
            set;
        }

        public
         Dominio RegimeCasamento
        {
            get;
            set;
        }

        public
         Dominio NacionalidadePessoa
        {
            get;
            set;
        }

        public
         DateTime? DataNascimento
        {
            get;
            set;
        }

        public
         Municipio MunicipioNascimento
        {
            get;
            set;
        }

        public
         Pais PaisNascimento
        {
            get;
            set;
        }

        public
         IList<CidadaniaBase> Cidadanias
        {
            get;
            set;
        }

        public
         bool Emancipado
        {
            get;
            set;
        }

        public
         bool Espolio
        {
            get;
            set;
        }

        public
         bool RecebeMensagem
        {
            get;
            set;
        }

        public
         bool DependenteEconomicamente
        {
            get;
            set;
        }

        public
         decimal RendaFamiliarMensal
        {
            get;
            set;
        }

        public
         PessoaFisicaCadastro Responsavel
        {
            get;
            set;
        }

        public
         PessoaFisicaCadastro Mae
        {
            get;
            set;
        }

        public
         PessoaFisicaCadastro Pai
        {
            get;
            set;
        }

        public
         PessoaFisicaCadastro Conjuge
        {
            get;
            set;
        }

        public
         Profissao ProfissaoConjuge
        {
            get;
            set;
        }
        public
         int QtdeDedependentes
        {
            get;
            set;
        }
        public
         IList<Dependente> Dependentes
        {
            get;
            set;
        }
        public
         IList<FazendaProdutorRural> Fazendas
        {
            get;
            set;
        }
        public
         IList<UnidadeProdutorRural> Unidades
        {
            get;
            set;
        }
        public
         IList<RebanhoProdutorRural> Rebanhos
        {
            get;
            set;
        }
        public
         IList<EstoqueAgricolaProdutorRural> EstoquesAgricolas
        {
            get;
            set;
        }
        public
         Escolaridade Escolaridade
        {
            get;
            set;
        }
        public
         DadoProfissional DadosProfissionais
        {
            get;
            set;
        }
        public
         IList<DocumentoIdentificacao> DocumentosIdentificacao
        {
            get;
            set;
        }

        public
         IList<PEP> PessoaExpostaPoliticamente
        {
            get;
            set;
        }

        public
         int Idade
        {
            get
            {
                if (!DataNascimento.HasValue ||
                    DataNascimento.Value == DateTime.MinValue)
                    return 0;

                var idade = DateTime.Now.Year - DataNascimento.Value.Year;

                if (DateTime.Now.Month < DataNascimento.Value.Month ||
                    (DateTime.Now.Month == DataNascimento.Value.Month &&
                     DateTime.Now.Day < DataNascimento.Value.Day))
                    idade--;

                return idade;
            }
        }

        public
         SituacaoLegal SituacaoLegal
        {
            get;
            set;
        }

        public
         bool ExpostoPoliticamente
        {
            get
            {
                return PessoaExpostaPoliticamente != null &&
                       PessoaExpostaPoliticamente.Count > 0;
            }
        }

        public
         IList<FonteRenda> FontesRenda
        {
            get;
            set;
        }

        public
         IList<MeioContato> MeiosDeContato
        {
            get;
            set;
        }

        public
         PortePessoaFisica Porte
        {
            get;
            set;
        }

        public
         Dominio FormaTratamento
        {
            get;
            set;
        }

        public
         override int GetHashCode()
        {
            if (this.Identificacao != 0)
                return this.Identificacao;
            else
                return base.GetHashCode();
        }

        public
         override bool Equals(object obj)
        { return Equals(obj as PessoaFisica); }

        public
         bool Equals(PessoaFisica other)
        {
            if (other == null) return false;

            if (this.CPF == null || other.CPF == null)
                return ReferenceEquals(this, other);

            bool isSame = this.CPF.Equals(other.CPF);

            if (this.DataNascimento.HasValue && other.DataNascimento.HasValue)
            {
                isSame = isSame &&
                         this.DataNascimento.Value.Equals(other.DataNascimento.Value);
            }

            if (this.Identificacao != 0 && other.Identificacao != 0)
            {
                isSame = isSame && this.Identificacao.Equals(other.Identificacao);
            }

            return isSame;
        }
    }
}  // namespace Entities