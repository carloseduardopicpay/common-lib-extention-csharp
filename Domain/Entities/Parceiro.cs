using System;
using System.Linq;
using System.Text;
using Volo.Abp;

namespace Domain.Entities
{
    public class Parceiro : ClassificacaoPessoa
    {
        protected Parceiro()
        { }

        public Parceiro(Pessoa pessoa) : this()
        {
            if (pessoa == null)
                throw new BusinessException("Pessoa não encontrada no Cliente!");

            StringBuilder camposObrigatorios = new StringBuilder();

            if (string.IsNullOrWhiteSpace(pessoa.NomeCompleto))
                camposObrigatorios.AppendLine("Nome");

            if (pessoa.Enderecos == null || pessoa.Enderecos.Count == 0 || pessoa.Enderecos.Where(x => x.IsEnderecoCorrespondencia == true).Count() == 0)
                camposObrigatorios.AppendLine("Endereço para Correspondência.");

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

                //if (pf.FontesRenda == null && pf.FontesRenda.Count == 0 || pf.FontesRenda.Sum(x => x.ValorRenda) == 0)
                //{
                //    camposObrigatorios.AppendLine("Renda");
                //}
            }
            else if (pessoa is PessoaJuridica)
            {
                var pj = (PessoaJuridica)pessoa;
                if (pj.CNPJ == null)
                    camposObrigatorios.AppendLine("CNPJ");

                if (pj.FormaConstituicao == null)
                    camposObrigatorios.AppendLine("Forma Constituição");

                //if (pj.DadosEconomicos == null || pj.DadosEconomicos.Count == 0)
                //{
                //    camposObrigatorios.AppendLine("Dados Econômicos e Faturamento Total");
                //}
                //else
                //{
                //    var ultimoDadoEconomico = pessoa.DadosEconomicos.OrderByDescending(x => x.PeriodoFinal).FirstOrDefault();
                //    decimal renda = ultimoDadoEconomico.ValorFaturamentoBruto;

                //    if (renda == 0)
                //    {
                //        camposObrigatorios.AppendLine("Faturamento Total");
                //    }

                //}

                ///Terminar campos obrigatorios                
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

        // Gerente transferido para a classe Pessoa.
        //public virtual int? IdentificacaoGerente { get; set; }

        public virtual Officer OfficerResponsavel
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

        public virtual Dominio TipoCategoria { get; set; }

        public virtual string CodigoExterno { get; set; }

        public virtual DateTime? DataInclusao { get; set; }
        //public virtual DateTime? DataInclusao { get; protected set; }

        public virtual DateTime DataAtualizacao { get; set; }

        public virtual DateTime? DataInativacao { get; set; }

        public virtual string ObservacaoInativacao { get; set; }

        public virtual TermoInternetBanking InternetBanking { get; set; }

        public virtual bool InvestidorInstituicional { get; set; }

        public virtual bool Factoring { get; set; }

        public virtual DateTime? DataValidadeCadastro { get; set; }

        public virtual DateTime? ClienteSistemaFinanceiroDesde { get; set; }

        public virtual string ClienteSistemaFinanceiro { get; set; }

        public virtual Dominio TipoFirma { get; set; }

        public virtual TipoRegimeTributacao TipoRegimeTributacao
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

        public virtual string UsuarioAlteracao { get; set; }

        public virtual ClassificacaoTributacaoCPMF ClassificacaoTributacaoCPMF { get; set; }
        public virtual ClassificacaoTributacaoIR ClassificacaoTributacaoIR { get; set; }
        public virtual ClassificacaoTributacaoIOF ClassificacaoTributacaoIOF { get; set; }

        public virtual TipoAtividade TipoAtividade { get; set; }

        /// <summary>
        /// Campo temporário onde está armazenado código do BOM. 
        /// Nessário implementar melhor forma de guardar códigos de cliente conforme origem.
        /// </summary>
        public virtual string CodigoExternoBOM { get; set; }

        public virtual string Banco { get; set; }


    }
}
