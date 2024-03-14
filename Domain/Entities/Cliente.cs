using System.Linq;
using System.Text;
using System;
using Volo.Abp;

namespace Domain.Entities
{
    public class Cliente : ClassificacaoPessoa
    {

        protected Cliente()
        {

        }

        public Cliente(Pessoa pessoa) : this()
        {
            if (pessoa == null)
                throw new BusinessException("Pessoa não encontrada no Cliente!");

            StringBuilder camposObrigatorios = new StringBuilder();

            if (string.IsNullOrWhiteSpace(pessoa.NomeCompleto))
                camposObrigatorios.AppendLine("Nome");

            if (pessoa.Enderecos == null || pessoa.Enderecos.Count == 0 || pessoa.Enderecos.Where(x => x.IsEnderecoCorrespondencia == true).Count() == 0)
                camposObrigatorios.AppendLine("Endereço para Correspondência.");


            //TODO: USAR FLUENTEVALIDATOR !
            if (pessoa is PessoaFisica)
            {
                var pf = (PessoaFisica)pessoa;

                if (pf.CPF == null)
                    camposObrigatorios.AppendLine("CPF");

                if (pf.Mae == null)
                    camposObrigatorios.AppendLine("Mãe");

                if (pf.PaisNascimento == null)
                    camposObrigatorios.AppendLine("País de Nascimento");

                if (!pf.DataNascimento.HasValue)
                    camposObrigatorios.AppendLine("Data de Nascimento");

                if (pf.MunicipioNascimento == null && pf.PaisNascimento != null && pf.PaisNascimento.Sigla == "BR")
                    camposObrigatorios.AppendLine("Municipio Nascimento");

                if (pf.Sexo == null)
                    camposObrigatorios.AppendLine("Sexo");

                if (pf.EstadoCivil == null)
                {
                    camposObrigatorios.AppendLine("Estado Civil");
                }
                else
                {
                    if (pf.EstadoCivil.Descricao == "CASADO")
                        if (pf.Conjuge == null)
                            camposObrigatorios.AppendLine("Cônjuge");
                }

                if (pf.DadosProfissionais == null)
                    camposObrigatorios.AppendLine("Dados Profissionais");

                if (pf.DocumentosIdentificacao == null || pf.DocumentosIdentificacao.Count == 0)
                    camposObrigatorios.AppendLine("Documento de Identificação");

            }
            else if (pessoa is PessoaJuridica)
            {
                var pj = (PessoaJuridica)pessoa;
                if (pj.CNPJ == null)
                    camposObrigatorios.AppendLine("CNPJ");

                if (pj.FormaConstituicao == null)
                    camposObrigatorios.AppendLine("Forma Constituição");

            }

            if (camposObrigatorios.Length > 0)
            {
                string _menssagem = "Para criar um cliente os devidos campos são obrigatórios:\n";

                throw new BusinessException(_menssagem + camposObrigatorios.ToString());
            }

            pessoa.DataRenovacao = DateTime.Today;
            Pessoa = pessoa;
            Pessoa.Classificacoes.Add(this);
        }

        public override bool ExigeFeitoConferido { get { return true; } protected set { } }

        public Officer OfficerResponsavel
        {
            get
            {
                if (this != null && this.Pessoa != null && this.Pessoa.OfficerResponsavel != null)
                {
                    return this.Pessoa.OfficerResponsavel;
                }
                else
                {
                    return null;
                }
            }
            protected set
            {
            }
        }

        public Dominio TipoCategoria { get; set; }

        public string CodigoExterno { get; set; }

        public DateTime? DataInclusao { get; set; }

        public DateTime DataAtualizacao { get; set; }

        public DateTime? DataInativacao { get; set; }

        public string ObservacaoInativacao { get; set; }

        public TermoInternetBanking InternetBanking { get; set; }

        public bool InvestidorInstituicional { get; set; }

        public bool Factoring { get; set; }

        public DateTime? DataValidadeCadastro { get; set; }

        public DateTime? ClienteSistemaFinanceiroDesde { get; set; }

        public string ClienteSistemaFinanceiro { get; set; }

        public Dominio TipoFirma { get; set; }

        public TipoRegimeTributacao TipoRegimeTributacao
        {
            get
            {
                if (this.Pessoa is PessoaJuridica)
                    return ((PessoaJuridica)this.Pessoa).TipoRegimeTributacao;
                else
                    return null;
            }
            protected set
            {
            }
        }

        public string UsuarioAlteracao { get; set; }

        public ClassificacaoTributacaoCPMF ClassificacaoTributacaoCPMF { get; set; }
        public ClassificacaoTributacaoIR ClassificacaoTributacaoIR { get; set; }
        public ClassificacaoTributacaoIOF ClassificacaoTributacaoIOF { get; set; }

        public TipoAtividade TipoAtividade { get; set; }

 
        public string CodigoExternoBOM { get; set; }

        public string Banco { get; set; }
    }

    public class ClassificacaoTributacaoCPMF : ClassificacaoTributacao
    {

    }

    public class ClassificacaoTributacaoIR : ClassificacaoTributacao
    {

    }

    public class ClassificacaoTributacaoIOF : ClassificacaoTributacao
    {

    }
}