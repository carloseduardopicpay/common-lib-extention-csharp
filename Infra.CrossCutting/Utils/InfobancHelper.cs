using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using static Infra.CrossCutting.Enums.Enums;

namespace Infra.CrossCutting.Utils
{
    internal static class InfobancHelper
    {
        public static List<DataParameter> ToDataParameter(this FonteRenda renda)
        {
            var parametros = new List<DataParameter>();

            if (renda.TipoFonteRenda != null)
                parametros.Add(new DataParameter("@RENDA", renda.TipoFonteRenda.Descricao));

            parametros.Add(new DataParameter("@DATA_RENDA", DateTime.Today));

            parametros.Add(new DataParameter("@VALOR", renda.ValorRenda));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Contato contato)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@NOME", contato.Nome));

            if (contato.Telefones != null && contato.Telefones.Count > 0)
            {
                var tel = contato.Telefones.FirstOrDefault();
                //parametros.Add(new DataParameter("@DDI_TELEFONE", tel.DDI));
                parametros.Add(new DataParameter("@DDD_TELEFONE", tel.DDD));
                parametros.Add(new DataParameter("@TELEFONE", tel.Numero));
                parametros.Add(new DataParameter("@RAMAL_TELEFONE", tel.Ramal));
            }
            else
            {
                //parametros.Add(new DataParameter("@DDI_TELEFONE", string.Empty));
                parametros.Add(new DataParameter("@DDD_TELEFONE", string.Empty));
                parametros.Add(new DataParameter("@TELEFONE", string.Empty));
                parametros.Add(new DataParameter("@RAMAL_TELEFONE", string.Empty));
            }


            if (contato.Emails != null && contato.Emails.Count > 0)
            {
                var email = contato.Emails.FirstOrDefault(e => e.Principal == true);

                if (email == null)
                    email = contato.Emails.FirstOrDefault();

                parametros.Add(new DataParameter("@EMAIL", email.Endereco));

            }
            parametros.Add(new DataParameter("@DEPARTAMENTO", ""));
            parametros.Add(new DataParameter("@OBSERVACAO", contato.Observacao));

            if (!string.IsNullOrEmpty(contato.Funcao))
                parametros.Add(new DataParameter("@FUNCAO", contato.Funcao));

            if (contato.Tipo != null)
                parametros.Add(new DataParameter("@ID_TIPO_CONTATO", contato.Tipo.Identificacao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Telefone telefone)
        {
            var parametros = new List<DataParameter>();

            // O Infobanc divide os telefones da pessoa em colunas no endereço (CELULAR E TELEFONE).
            if (telefone.Tipo != null && telefone.Tipo.Descricao.ToUpper().Equals("CELULAR"))
            {
                parametros.Add(new DataParameter("@DDI_CELULAR", telefone.DDI));
                parametros.Add(new DataParameter("@DDD_CELULAR", telefone.DDD));
                parametros.Add(new DataParameter("@CELULAR", telefone.Numero));
            }
            else
            {
                parametros.Add(new DataParameter("@DDI_TELEFONE", telefone.DDI));
                parametros.Add(new DataParameter("@DDD_TELEFONE", telefone.DDD));
                parametros.Add(new DataParameter("@TELEFONE", telefone.Numero));
                parametros.Add(new DataParameter("@RAMAL_TELEFONE", telefone.Ramal));
            }

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Endereco endereco)
        {
            var parametros = new List<DataParameter>();

            if (endereco.Tipo != null)
                parametros.Add(new DataParameter("@ID_TIPO_ENDERECO", endereco.Tipo.Identificacao));

            string logradouro = endereco.Logradouro;

            // Concatena o tipo de logradouro com o nome do logradouro;
            if (endereco.TipoLogradouro != null)
            {
                logradouro = string.Concat(endereco.TipoLogradouro.Descricao, " ", logradouro);
            }

            parametros.Add(new DataParameter("@ENDERECO", logradouro));
            parametros.Add(new DataParameter("@NUMERO", endereco.Numero));
            parametros.Add(new DataParameter("@COMPLEMENTO", endereco.Complemento));
            parametros.Add(new DataParameter("@BAIRRO", endereco.Bairro));

            if (endereco.Municipio != null)
            {
                parametros.Add(new DataParameter("@ID_MUNICIPIO", endereco.Municipio.Identificacao));
            }

            parametros.Add(new DataParameter("@CEP", endereco.CEP));

            if (endereco.Tipo != null && endereco.Telefones != null && endereco.Telefones.Count > 0)
            {
                var celular = endereco.Telefones.LastOrDefault(x => x.Tipo != null && x.Tipo.Descricao.ToUpper().Equals("CELULAR"));
                if (celular != null)
                    parametros.AddRange(celular.ToDataParameter());

                var telefone = endereco.Telefones.LastOrDefault(x => x.Tipo != null && x.Tipo.Descricao.Equals(endereco.Tipo.Descricao));
                if (telefone != null)
                    parametros.AddRange(telefone.ToDataParameter());
            }

            parametros.Add(new DataParameter("@CAIXAPOSTAL", endereco.CaixaPostal));
            parametros.Add(new DataParameter("@IC_CORRESPONDENCIA", endereco.IsEnderecoCorrespondencia));
            parametros.Add(new DataParameter("@IC_INFORME_RENDIMENTOS", endereco.IsEnderecoCorrespondencia));

            if (endereco.TipoPosse != null)
                parametros.Add(new DataParameter("@ID_DOMINIO_TIPO_IMOVEL_RESIDENCIAL", endereco.TipoPosse.Identificacao));

            if (!string.IsNullOrEmpty(endereco.ResideDesdeAno))
            {
                var tempoResidencia = Convert.ToInt32(endereco.ResideDesdeAno);

                if (tempoResidencia > 0)
                    tempoResidencia = DateTime.Today.Year - tempoResidencia;

                parametros.Add(new DataParameter("@TEMPORESIDENCIA", tempoResidencia));
            }

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Documento documento)
        {
            var parametros = new List<DataParameter>();

            if (documento == null)
            {
                return parametros;
            }

            if (documento.Tipo != null)
                parametros.Add(new DataParameter("@ID_TP_DOCUMENTO", documento.Tipo.Identificacao));

            if (documento.Status != null)
                parametros.Add(new DataParameter("@IC_ENTREGUE", !documento.Status.GeraPendencia));
            else
                parametros.Add(new DataParameter("@IC_ENTREGUE", false));

            parametros.Add(new DataParameter("@DS_OBSERVACAO", documento.Observacao));

            parametros.Add(new DataParameter("@DATA_BASE", documento.DataInclusao));

            parametros.Add(new DataParameter("@DATA_VALIDADE", documento.DataValidade));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Restricao restricao)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@ID_RESTRICAO", restricao.Identificacao));

            if (restricao.TipoRestricao != null)
                parametros.Add(new DataParameter("@CODIGO_RESTRICAO", restricao.TipoRestricao.Identificacao));

            if (restricao.Pessoa != null)
            {
                if (restricao.Pessoa is PessoaFisica)
                {
                    var pf = restricao.Pessoa as PessoaFisica;

                    if (pf.CPF != null)
                    {
                        parametros.Add(new DataParameter("@CGC_CPF", pf.CPF.Numero));
                        parametros.Add(new DataParameter("@SEQUENCIA", pf.CPF.Sequencia));
                    }
                }
                else if (restricao.Pessoa is PessoaJuridica)
                {
                    var pj = restricao.Pessoa as PessoaJuridica;

                    if (pj.CNPJ != null)
                    {
                        parametros.Add(new DataParameter("@CGC_CPF", pj.CNPJ.Numero));
                        parametros.Add(new DataParameter("@SEQUENCIA", pj.CNPJ.Sequencia));
                    }
                }
            }

            parametros.Add(new DataParameter("@DATA_OCORRENCIA", restricao.DataOcorrencia));
            parametros.Add(new DataParameter("@DATA_REGISTRO", restricao.DataRegistro));

            if (restricao.DominioFonteInformacao != null)
            {
                parametros.Add(new DataParameter("@FONTE_INFORMACAO", restricao.DominioFonteInformacao.Descricao));
            }
            else if (!string.IsNullOrEmpty(restricao.FonteInformacao))
                parametros.Add(new DataParameter("@FONTE_INFORMACAO", restricao.FonteInformacao));

            parametros.Add(new DataParameter("@NUMERO_DOCUMENTO", restricao.NumeroDocumento));
            parametros.Add(new DataParameter("@VALOR", restricao.ValorRestricao));
            parametros.Add(new DataParameter("@DATA_REGULARIZACAO", restricao.DataRegularizacao));
            parametros.Add(new DataParameter("@DATA_CANCELAMENTO", restricao.DataCancelado));
            parametros.Add(new DataParameter("@OBSERVACOES", restricao.Observacao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Vinculo participante)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@NOME_GRUPO", participante.Pessoa.NomeCompleto));

            if (participante.TipoRelacionamento != null)
                parametros.Add(new DataParameter("@TIPO_RELACIONAMENTO", participante.TipoRelacionamento.Descricao));

            if (participante.Cargo != null)
                parametros.Add(new DataParameter("@ID_FUNCAO", participante.Cargo.Identificacao));

            // Grupo de Participação
            if (participante.Pessoa is PessoaFisica)
            {
                var grupoPF = participante.Pessoa as PessoaFisica;
                if (grupoPF.CPF != null)
                {
                    parametros.Add(new DataParameter("@CGC_CPF_GRUPO", grupoPF.CPF.Numero));
                    parametros.Add(new DataParameter("@SEQUENCIA_GRUPO", grupoPF.CPF.Sequencia));
                }
            }
            else if (participante.Pessoa is PessoaJuridica)
            {
                var grupoPJ = participante.Pessoa as PessoaJuridica;
                if (grupoPJ.CNPJ != null)
                {
                    parametros.Add(new DataParameter("@CGC_CPF_GRUPO", grupoPJ.CNPJ.Numero));
                    parametros.Add(new DataParameter("@SEQUENCIA_GRUPO", grupoPJ.CNPJ.Sequencia));
                }
            }

            // Vinculo
            if (participante.PessoaParticipante is PessoaFisica)
            {
                var participantePF = participante.PessoaParticipante as PessoaFisica;
                if (participantePF.CPF != null)
                {
                    parametros.Add(new DataParameter("@CGC_CPF_PARTICIPANTE", participantePF.CPF.Numero));
                    parametros.Add(new DataParameter("@SEQUENCIA_PARTICIPANTE", participantePF.CPF.Sequencia));
                }
            }
            else if (participante.PessoaParticipante is PessoaJuridica)
            {
                var participantePJ = participante.PessoaParticipante as PessoaJuridica;
                if (participantePJ.CNPJ != null)
                {
                    parametros.Add(new DataParameter("@CGC_CPF_PARTICIPANTE", participantePJ.CNPJ.Numero));
                    parametros.Add(new DataParameter("@SEQUENCIA_PARTICIPANTE", participantePJ.CNPJ.Sequencia));
                }
            }

            parametros.Add(new DataParameter("@PERCENT_PARTICIPACAO", participante.PercentualParticipacao));
            parametros.Add(new DataParameter("@PERCENT_CAPITAL_VOTANTE", participante.PercentualCapitalVotante));

            parametros.Add(new DataParameter("@VALOR_CAPITAL_SOCIAL", participante.CapitalSocial));

            parametros.Add(new DataParameter("@SEDE", participante.Sede));

            parametros.Add(new DataParameter("@DATA_ENTRADA", participante.DataEntrada));
            parametros.Add(new DataParameter("@DATA_SAIDA", participante.DataSaida));
            parametros.Add(new DataParameter("@NOME_USUARIO", participante.Pessoa.UsuarioAlteracao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this ClassificacaoPessoa papel)
        {
            List<DataParameter> parameters;

            if (papel is Cliente)
                parameters = ((Cliente)papel).ToDataParameter();
            else
                parameters = new List<DataParameter>();

            return parameters;
        }

        public static List<DataParameter> ToDataParameter(this Cliente cliente)
        {
            var parametros = new List<DataParameter>();

            if (cliente == null || cliente.Pessoa == null)
                return parametros;

            parametros = cliente.Pessoa.ToDataParameter();
            parametros.Add(new DataParameter("@CODCLIENTE", cliente.CodigoExterno));
            parametros.Add(new DataParameter("@CODCLIENTEBOM", cliente.CodigoExternoBOM));

            parametros.Add(new DataParameter("@DS_TIPO_CLASSIFICACAO", cliente.Descricao));

            //TODO: Implementar demais parametros.
            parametros.Add(new DataParameter("@NOME", cliente.Nome));
            parametros.Add(new DataParameter("@DATACADASTRO", cliente.DataInclusao));
            parametros.Add(new DataParameter("@DATAVENCIMENTO", cliente.DataValidadeCadastro));

            if (cliente.Pessoa is PessoaFisica)
            {
                var pf = cliente.Pessoa as PessoaFisica;

                if (pf.DocumentosIdentificacao != null && pf.DocumentosIdentificacao.Count > 0)
                {
                    var docIdentidade = pf.DocumentosIdentificacao.LastOrDefault();
                    parametros.AddRange(docIdentidade.ToDataParameter());
                }
            }
            else if (cliente.Pessoa is PessoaJuridica)
            {
                var pj = cliente.Pessoa as PessoaJuridica;

                if (pj.TipoRegimeTributacao != null && !string.IsNullOrEmpty(pj.TipoRegimeTributacao.Descricao))
                    parametros.Add(new DataParameter("@DS_REGIME_TRIBUTACAO", pj.TipoRegimeTributacao.Descricao.ToUpper()));
            }

            if (cliente.TipoCategoria != null)
                parametros.Add(new DataParameter("@ID_DOMINIO_TIPO_CATEGORIA", cliente.TipoCategoria.Identificacao));

            if (cliente.ClassificacaoTributacaoIR != null)
                parametros.Add(new DataParameter("@ID_CLASSIFICACAO_TRIBUTACAO_IR", cliente.ClassificacaoTributacaoIR.Identificacao));

            if (cliente.TipoAtividade != null)
                parametros.Add(new DataParameter("@ID_TIPO_ATIVIDADE", cliente.TipoAtividade.Identificacao));

            parametros.Add(new DataParameter("@IC_FACTORING", cliente.Factoring));

            if (cliente.InternetBanking != null)
                parametros.Add(new DataParameter("@CD_CLIENTE_HOMEBANK", cliente.InternetBanking.ToParameter()));

            if (cliente.Pessoa.DataRenovacao.HasValidDateValue())
                parametros.Add(new DataParameter("@DATARENOVACAO", cliente.Pessoa.DataRenovacao.Value));

            if (cliente.ClassificacaoTributacaoIOF != null)
                parametros.Add(new DataParameter("@ID_CLASSIFICACAO_TRIBUTACAO_IOF", cliente.ClassificacaoTributacaoIOF.Identificacao));

            parametros.Add(new DataParameter("@IC_INVESTIDOR_INSTITUCIONAL", cliente.InvestidorInstituicional));

            if (cliente.TipoFirma != null)
                parametros.Add(new DataParameter("@DS_TIPO_FIRMA", cliente.TipoFirma.Descricao));

            if (cliente.DataInativacao.HasValidDateValue())
                parametros.Add(new DataParameter("@DT_INATIVACAO", cliente.DataInativacao.Value));

            if (cliente.ClienteSistemaFinanceiroDesde.HasValidDateValue())
            {
                parametros.Add(new DataParameter("@DT_SISTEMA_FINANCEIRO_DESDE", cliente.ClienteSistemaFinanceiroDesde.Value));
                parametros.Add(new DataParameter("@OBSCLIENTEDESDE", cliente.ClienteSistemaFinanceiro));
            }

            if (cliente.Pessoa.Gerente != null)
            {
                parametros.Add(new DataParameter("@ID_PAPEL_COMERCIAL_GERENTE", cliente.Pessoa.Gerente.Id));
            }

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Parceiro parceiro)
        {
            var parametros = new List<DataParameter>();

            if (parceiro == null || parceiro.Pessoa == null)
                return parametros;

            parametros = parceiro.Pessoa.ToDataParameter();
            parametros.Add(new DataParameter("@CODCLIENTE", parceiro.CodigoExterno));
            parametros.Add(new DataParameter("@CODCLIENTEBOM", parceiro.CodigoExternoBOM));

            parametros.Add(new DataParameter("@DS_TIPO_CLASSIFICACAO", parceiro.Descricao));

            //TODO: Implementar demais parametros.
            parametros.Add(new DataParameter("@NOME", parceiro.Nome));
            parametros.Add(new DataParameter("@DATACADASTRO", parceiro.DataInclusao));
            parametros.Add(new DataParameter("@DATAVENCIMENTO", parceiro.DataValidadeCadastro));

            if (parceiro.Pessoa is PessoaFisica)
            {
                var pf = parceiro.Pessoa as PessoaFisica;

                if (pf.DocumentosIdentificacao != null && pf.DocumentosIdentificacao.Count > 0)
                {
                    var docIdentidade = pf.DocumentosIdentificacao.LastOrDefault();
                    parametros.AddRange(docIdentidade.ToDataParameter());
                }
            }
            else if (parceiro.Pessoa is PessoaJuridica)
            {
                var pj = parceiro.Pessoa as PessoaJuridica;

                if (pj.TipoRegimeTributacao != null && !string.IsNullOrEmpty(pj.TipoRegimeTributacao.Descricao))
                    parametros.Add(new DataParameter("@DS_REGIME_TRIBUTACAO", pj.TipoRegimeTributacao.Descricao.ToUpper()));
            }

            if (parceiro.TipoCategoria != null)
                parametros.Add(new DataParameter("@ID_DOMINIO_TIPO_CATEGORIA", parceiro.TipoCategoria.Identificacao));

            if (parceiro.ClassificacaoTributacaoIR != null)
                parametros.Add(new DataParameter("@ID_CLASSIFICACAO_TRIBUTACAO_IR", parceiro.ClassificacaoTributacaoIR.Identificacao));

            if (parceiro.TipoAtividade != null)
                parametros.Add(new DataParameter("@ID_TIPO_ATIVIDADE", parceiro.TipoAtividade.Identificacao));

            parametros.Add(new DataParameter("@IC_FACTORING", parceiro.Factoring));

            if (parceiro.InternetBanking != null)
                parametros.Add(new DataParameter("@CD_CLIENTE_HOMEBANK", parceiro.InternetBanking.ToParameter()));

            if (parceiro.Pessoa.DataRenovacao.HasValidDateValue())
                parametros.Add(new DataParameter("@DATARENOVACAO", parceiro.Pessoa.DataRenovacao.Value));

            if (parceiro.ClassificacaoTributacaoIOF != null)
                parametros.Add(new DataParameter("@ID_CLASSIFICACAO_TRIBUTACAO_IOF", parceiro.ClassificacaoTributacaoIOF.Identificacao));

            parametros.Add(new DataParameter("@IC_INVESTIDOR_INSTITUCIONAL", parceiro.InvestidorInstituicional));

            if (parceiro.TipoFirma != null)
                parametros.Add(new DataParameter("@DS_TIPO_FIRMA", parceiro.TipoFirma.Descricao));

            if (parceiro.DataInativacao.HasValidDateValue())
                parametros.Add(new DataParameter("@DT_INATIVACAO", parceiro.DataInativacao.Value));

            if (parceiro.ClienteSistemaFinanceiroDesde.HasValidDateValue())
            {
                parametros.Add(new DataParameter("@DT_SISTEMA_FINANCEIRO_DESDE", parceiro.ClienteSistemaFinanceiroDesde.Value));
                parametros.Add(new DataParameter("@OBSCLIENTEDESDE", parceiro.ClienteSistemaFinanceiro));
            }

            if (parceiro.Pessoa.Gerente != null)
            {
                parametros.Add(new DataParameter("@ID_PAPEL_COMERCIAL_GERENTE", parceiro.Pessoa.Gerente.Id));
            }

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Pessoa pessoa)
        {
            var parametros = new List<DataParameter>();

            if (pessoa is PessoaFisica)
            {
                parametros.Add(new DataParameter("@CGC_CPF", ((PessoaFisica)pessoa).CPF.Numero));
                parametros.Add(new DataParameter("@SEQUENCIA", ((PessoaFisica)pessoa).CPF.Sequencia));
                parametros.Add(new DataParameter("@TIPOPESSOA", "F"));
            }
            else
            {
                parametros.Add(new DataParameter("@CGC_CPF", ((PessoaJuridica)pessoa).CNPJ.Numero));
                parametros.Add(new DataParameter("@SEQUENCIA", 0));
                parametros.Add(new DataParameter("@TIPOPESSOA", "J"));
            }

            parametros.Add(new DataParameter("@NOME_USUARIO", pessoa.UsuarioAlteracao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this PessoaFisica pessoa)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@TP_PESSOA", "F"));
            parametros.Add(new DataParameter("@CGC_CPF", pessoa.CPF.Numero));
            parametros.Add(new DataParameter("@SEQUENCIA", pessoa.CPF.Sequencia));
            parametros.Add(new DataParameter("@NOME_USUARIO", pessoa.UsuarioAlteracao));
            parametros.Add(new DataParameter("@NM_PESSOA_FANTASIA", !string.IsNullOrEmpty(pessoa.Codinome) ? pessoa.Codinome : string.Empty));

            parametros.Add(new DataParameter("@NM_PESSOA_COMPLETO", pessoa.Nome.ToString()));
            parametros.Add(new DataParameter("@NM_PESSOA", pessoa.NomeAbreviado));

            parametros.Add(new DataParameter("@DT_CADASTRO", pessoa.DataInclusao));

            //if (pessoa.PermiteConsultaBACEN_NCR != OpcaoColigada.NAOAUTORIZADO)
            //    parametros.Add(new DataParameter("@CD_AUTORIZA_CONSULTA_BACEN", pessoa.PermiteConsultaBACEN_NCR.ToParameter()));

            if (pessoa.PermiteConsultaBACEN_NCR != OpcaoColigada.NAOAUTORIZADO.ToString())
                parametros.Add(new DataParameter("@CD_AUTORIZA_CONSULTA_BACEN", pessoa.PermiteConsultaBACEN_NCR));

            parametros.Add(new DataParameter("@IC_AUTORIZA_BACEN_PCAM", pessoa.PermiteConsultaBACEN_PCAM));

            parametros.Add(new DataParameter("@DT_RENOVACAO", pessoa.DataRenovacao));

            parametros.Add(new DataParameter("@ENDERECO_ELETRONICO", !string.IsNullOrEmpty(pessoa.WebSite) ? pessoa.WebSite.ToLower() : string.Empty));

            if (pessoa.Sexo != null)
                parametros.Add(new DataParameter("@DS_SEXO", pessoa.Sexo.Descricao));

            if (pessoa.EstadoCivil != null)
                parametros.Add(new DataParameter("@DS_ESTADO_CIVIL", pessoa.EstadoCivil.Descricao));

            parametros.Add(new DataParameter("@DT_NASCIMENTO", pessoa.DataNascimento));
            parametros.Add(new DataParameter("@IC_MENOR_EMANCIPADO", pessoa.Emancipado));
            parametros.Add(new DataParameter("@IC_ESPOLIO", pessoa.Espolio));


            if (pessoa.Pai != null)
                parametros.Add(new DataParameter("@NM_PAI", pessoa.Pai.Nome));

            if (pessoa.Mae != null)
                parametros.Add(new DataParameter("@NM_MAE", pessoa.Mae.Nome));

            if (pessoa.Conjuge != null)
            {
                parametros.Add(new DataParameter("@NM_CONJUGE", pessoa.Conjuge.Nome));

                if (pessoa.Conjuge.CPF != null)
                    parametros.Add(new DataParameter("@CPF_CONJUGE", pessoa.Conjuge.CPF.Numero));
            }

            if (pessoa.Escolaridade != null && pessoa.Escolaridade.TipoEscolaridade != null)
                parametros.Add(new DataParameter("@DS_ESCOLARIDADE", pessoa.Escolaridade.TipoEscolaridade.Descricao));

            if (pessoa.DadosProfissionais != null)
            {
                if (pessoa.DadosProfissionais.Profissao != null)
                    parametros.Add(new DataParameter("@ID_PROFISSAO", pessoa.DadosProfissionais.Profissao.Identificacao));

                if (pessoa.DadosProfissionais.NaturezaOcupacao != null)
                    parametros.Add(new DataParameter("@CD_NATUREZA_OCUPACAO", pessoa.DadosProfissionais.NaturezaOcupacao.Codigo));
            }

            if (pessoa.PaisNascimento != null)
            {
                parametros.Add(new DataParameter("@IC_EXTRANGEIRO", pessoa.PaisNascimento.Sigla.Equals("BR")));
                parametros.Add(new DataParameter("@ID_PAIS_NASCIMENTO", pessoa.PaisNascimento.Identificacao));
            }
            else if (pessoa.Cidadanias != null)
            {
                var cidadaniaBrasileira = pessoa.Cidadanias.FirstOrDefault(x => x.Tipo == "CIDADANIA" && x.Pais.Sigla.Equals("BR"));

                parametros.Add(new DataParameter("@IC_EXTRANGEIRO", cidadaniaBrasileira == null));
            }

            if (pessoa.MunicipioNascimento != null)
                parametros.Add(new DataParameter("@ID_MUNICIPIO_NASC", pessoa.MunicipioNascimento.Identificacao));

            /*
            * Alterado conceito do envio de rendas ao Infobank.
            * A renda no Infobank deve ser a soma do primeiro tipo de renda na seguinte ordem:
            * Comprovada, Declarada, Presumida por crédito ou Presumida por modelo
            */
            if (pessoa.FontesRenda != null && pessoa.FontesRenda.Count > 0)
            {
                decimal valorRenda = 0;
                string[] seqTipoInformacao = { "COMP", "DECL", "PRES_CRED", "PRES_MOD" };

                for (int i = 0; i < seqTipoInformacao.Length; i++)
                {
                    if (pessoa.FontesRenda.Any(r => r.TipoInformacao != null && r.TipoInformacao.Mnemonico.Equals(seqTipoInformacao[i])))
                    {
                        valorRenda = pessoa.FontesRenda
                                            .Where(r => r.TipoInformacao != null && r.TipoInformacao.Mnemonico.Equals(seqTipoInformacao[i]))
                                            .Sum(x => x.ValorRenda);
                        break;
                    }
                }

                parametros.Add(new DataParameter("@VL_RENDA", valorRenda));
            }

            //if (pessoa.SetorAtividade != null)
            //    parametros.Add(new DataParameter("@ID_SETOR", pessoa.SetorAtividade.Identificacao));

            if (pessoa.Porte != null)
                parametros.Add(new DataParameter("@DS_PORTE_PESSOA", pessoa.Porte.Descricao));

            var emails = pessoa.MeiosDeContato.OfType<Email>();

            if (emails != null && emails.Count() > 0)
            {
                var emailInfo = emails.FirstOrDefault(e => e.Principal == true);

                if (emailInfo == null)
                    emailInfo = emails.FirstOrDefault();

                parametros.Add(new DataParameter("@EMAIL", emailInfo.Endereco.ToLower()));
            }

            if (pessoa.DocumentosIdentificacao != null && pessoa.DocumentosIdentificacao.Count > 0)
            {
                var docIdentidade = pessoa.DocumentosIdentificacao.FirstOrDefault();

                if (docIdentidade != null)
                    parametros.AddRange(docIdentidade.ToDataParameter());

            }

            if (pessoa.QtdeDedependentes > 0)
                parametros.Add(new DataParameter("@NO_DEPENDENTES", pessoa.QtdeDedependentes));
            else
                if (pessoa.Dependentes != null)
                parametros.Add(new DataParameter("@NO_DEPENDENTES", pessoa.Dependentes.Count()));

            if (pessoa.DadosPatrimoniais != null)
                parametros.AddRange(pessoa.DadosPatrimoniais.ToDataParameter());

            if (pessoa.OfficerResponsavel != null)
                parametros.Add(new DataParameter("@ID_PAPEL_COMERCIAL_OFFICER", pessoa.OfficerResponsavel.Id));

            return parametros;

        }

        public static List<DataParameter> ToDataParameter(this PessoaJuridica pessoa)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@TP_PESSOA", "J"));
            parametros.Add(new DataParameter("@CGC_CPF", pessoa.CNPJ.Numero));
            parametros.Add(new DataParameter("@SEQUENCIA", 0));
            parametros.Add(new DataParameter("@NOME_USUARIO", pessoa.UsuarioAlteracao));
            parametros.Add(new DataParameter("@NM_PESSOA_FANTASIA", !string.IsNullOrEmpty(pessoa.NomeFantasia) ? pessoa.NomeFantasia : string.Empty));
            parametros.Add(new DataParameter("@DT_NASCIMENTO", pessoa.DataFundacao));

            parametros.Add(new DataParameter("@NM_PESSOA_COMPLETO", pessoa.Nome.ToString()));
            parametros.Add(new DataParameter("@NM_PESSOA", pessoa.NomeAbreviado));

            if (pessoa.Nacionalidade != null)
            {
                parametros.Add(new DataParameter("@IC_EXTRANGEIRO", pessoa.Nacionalidade.Sigla.Equals("BR")));
                parametros.Add(new DataParameter("@ID_PAIS_NASCIMENTO", pessoa.Nacionalidade.Identificacao));
            }

            if (pessoa.Municipio != null)
                parametros.Add(new DataParameter("@ID_MUNICIPIO_NASC", pessoa.Municipio.Identificacao));

            if (pessoa.CapitalSocial != null)
            {
                if (pessoa.CapitalSocial.Tipo != null)
                    parametros.Add(new DataParameter("@DS_CAPITAL_SOCIAL", pessoa.CapitalSocial.Tipo.Descricao));

                parametros.Add(new DataParameter("@IC_CAPITAL_ABERTO", pessoa.CapitalSocial.CapitalAberto));
                parametros.Add(new DataParameter("@VL_CAPITAL_ABERTO", pessoa.CapitalSocial.ValorCapitalAberto));
                parametros.Add(new DataParameter("@VL_CAPITAL_SOCIAL", pessoa.CapitalSocial.Valor));
            }

            parametros.Add(new DataParameter("@DT_CADASTRO", pessoa.DataInclusao));

            if (pessoa.PermiteConsultaBACEN_NCR != OpcaoColigada.NAOAUTORIZADO.ToString())
                parametros.Add(new DataParameter("@CD_AUTORIZA_CONSULTA_BACEN", pessoa.PermiteConsultaBACEN_NCR));

            parametros.Add(new DataParameter("@IC_AUTORIZA_BACEN_PCAM", pessoa.PermiteConsultaBACEN_PCAM));

            parametros.Add(new DataParameter("@DT_RENOVACAO", pessoa.DataRenovacao));

            parametros.Add(new DataParameter("@ENDERECO_ELETRONICO", pessoa.WebSite));

            if (pessoa.SetoresDeAtividades != null && pessoa.SetoresDeAtividades.Count > 0)
            {
                var atividadePrincipal = pessoa.SetoresDeAtividades.FirstOrDefault(x => x.CNAE != null && x.Principal == true);

                if (atividadePrincipal != null)
                {
                    parametros.Add(new DataParameter("@ID_SETOR", atividadePrincipal.CNAE.Identificacao));

                    if (atividadePrincipal.CNAE.TipoInstituicao != null)
                        parametros.Add(new DataParameter("@ID_TIPO_INSTITUICAO", atividadePrincipal.CNAE.TipoInstituicao.Identificacao));
                }
            }

            if (!string.IsNullOrEmpty(pessoa.NumeroContrato))
                parametros.Add(new DataParameter("@NUMREGISTRO", pessoa.NumeroContrato));

            var contato = pessoa.Contatos.FirstOrDefault(x => x.Emails != null && x.Emails.Count > 0);

            if (contato != null)
            {
                var email = contato.Emails.FirstOrDefault(e => e.Principal == true);

                if (email == null)
                    email = contato.Emails.FirstOrDefault();

                if (email != null)
                    parametros.Add(new DataParameter("@EMAIL", email.Endereco.ToLower()));
            }

            if (pessoa.Porte != null)
                parametros.Add(new DataParameter("@DS_PORTE_PESSOA", pessoa.Porte.Descricao));

            if (pessoa.TipoRegimeTributacao != null && !string.IsNullOrEmpty(pessoa.TipoRegimeTributacao.Descricao))
                parametros.Add(new DataParameter("@DS_REGIME_TRIBUTACAO", pessoa.TipoRegimeTributacao.Descricao.ToUpper()));

            if (pessoa.TipoControleBACEN != null)
            {
                parametros.Add(new DataParameter("@DS_CONTROLE", pessoa.TipoControleBACEN.Descricao));
            }

            if (pessoa.FormaConstituicao != null)
            {
                parametros.Add(new DataParameter("@DS_FORMA_CONSTITUICAO", pessoa.FormaConstituicao.Descricao));
            }

            if (pessoa.DadosEconomicos != null && pessoa.DadosEconomicos.Count > 0)
            {
                var ultimoDadoEconomico = pessoa.DadosEconomicos.OrderByDescending(x => x.PeriodoFinal).FirstOrDefault();

                parametros.Add(new DataParameter("@COMPRAS_ULTIMO_EXERCICIO", ultimoDadoEconomico.ValorCompras));
                parametros.Add(new DataParameter("@VENDAS_ULTIMO_EXERCICIO", ultimoDadoEconomico.ValorVendas));
                parametros.Add(new DataParameter("@VL_RENDA", ultimoDadoEconomico.ValorFaturamentoLiquido));
                parametros.Add(new DataParameter("@VL_RENDA_TOTAL", ultimoDadoEconomico.ValorFaturamentoBruto));

                parametros.Add(new DataParameter("@IC_IMPORTA", ultimoDadoEconomico.Importa));
                parametros.Add(new DataParameter("@IC_EXPORTA", ultimoDadoEconomico.Exporta));
                parametros.Add(new DataParameter("@QTD_FUNCIONARIOS", ultimoDadoEconomico.QuantidadeFuncionarios));
            }

            if (pessoa.ControleAcionario != null)
            {
                parametros.Add(new DataParameter("@DS_CONTROLE_ACIONARIO", pessoa.ControleAcionario.Descricao));
            }

            if (pessoa.NaturezaEmpresa != null)
            {
                parametros.Add(new DataParameter("@ID_NATUREZA_EMPRESA", pessoa.NaturezaEmpresa.Identificacao));
            }

            parametros.Add(new DataParameter("@DT_ULTIMA_ALTERACAO_CONTRATUAL", pessoa.DataUltAlterContratual));

            parametros.Add(new DataParameter("@CODCETIP", pessoa.Cetip));
            parametros.Add(new DataParameter("@CODSELIC", pessoa.Selic));
            parametros.Add(new DataParameter("@CODBANCO", pessoa.Compensacao));
            parametros.Add(new DataParameter("@CODISPB", pessoa.ISPB));

            if (pessoa.DadosPatrimoniais != null)
                parametros.AddRange(pessoa.DadosPatrimoniais.ToDataParameter());

            if (pessoa.OfficerResponsavel != null)
                parametros.Add(new DataParameter("@ID_PAPEL_COMERCIAL_OFFICER", pessoa.OfficerResponsavel.Id));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this DocumentoIdentificacao docIdentidade)
        {
            var parametros = new List<DataParameter>();

            parametros.Add(new DataParameter("@NO_DOC_IDENTIDADE", docIdentidade.Numero));

            string tipoDocumento = docIdentidade.GetType().Name;
            if (docIdentidade is DocumentoIdentificacaoNaoDefinido)
                tipoDocumento = "OUTROS";

            parametros.Add(new DataParameter("@CD_TIPO_DOC_IDENTIDADE", tipoDocumento.ToUpper()));

            if (docIdentidade.OrgaoEmissor != null)
                parametros.Add(new DataParameter("@CD_SIGLA_ORGAO_EMISSOR", docIdentidade.OrgaoEmissor.Sigla));

            parametros.Add(new DataParameter("@DT_DOC_IDENTIDADE", docIdentidade.DataEmissao));

            string ufExpedicao = null;

            if (docIdentidade is RG)
            {
                var rg = ((RG)docIdentidade);

                if (rg != null && rg.UF != null)
                    ufExpedicao = ((RG)docIdentidade).UF.Sigla;
            }
            else if (docIdentidade is CNH)
            {
                var cnh = ((CNH)docIdentidade);

                if (cnh != null && cnh.UF != null)
                    ufExpedicao = cnh.UF.Sigla;
            }
            else if (docIdentidade is Passaporte)
            {
                var passaporte = ((Passaporte)docIdentidade);

                if (passaporte != null && passaporte.UF != null)
                    ufExpedicao = ((Passaporte)docIdentidade).UF.Sigla;
            }

            if (!string.IsNullOrEmpty(ufExpedicao))
                parametros.Add(new DataParameter("@UF_DOC_IDENTIDADE", ufExpedicao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this DadoProfissional dadoProfissional)
        {
            var parametros = new List<DataParameter>();

            if (dadoProfissional == null)
                return parametros;

            if (dadoProfissional.Empresa != null)
            {
                parametros.Add(new DataParameter("@NOME_EMPREGADOR", dadoProfissional.Empresa.Nome));

                if (dadoProfissional.Empresa.CNPJ != null)
                    parametros.Add(new DataParameter("@CNPJ_EMPREGADOR", dadoProfissional.Empresa.CNPJ.Numero));
            }

            string cargo = "";
            if (dadoProfissional.Cargo != null)
                cargo = dadoProfissional.Cargo.Descricao;

            parametros.Add(new DataParameter("@CARGO", cargo));

            parametros.Add(new DataParameter("@DATA_ENTRADA", dadoProfissional.DataAdmissao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this DadoPatrimonial dadoPatrimonial)
        {
            var parametros = new List<DataParameter>();

            if (dadoPatrimonial == null)
                return parametros;

            decimal valorPatrimonio = dadoPatrimonial.ValorImoveisRurais + dadoPatrimonial.ValorImoveisUrbanos + dadoPatrimonial.ValorImovelResidencial + dadoPatrimonial.ValorVeiculos;

            valorPatrimonio = valorPatrimonio > 0 ? valorPatrimonio : dadoPatrimonial.ValorPatrimonioTotal;
            parametros.Add(new DataParameter("@PATRIMONIO_LIQUIDO", valorPatrimonio));

            return parametros;
        }

        private static string ToParameter(this OpcaoColigada opcao)
        {
            string parametro = string.Empty;

            if (opcao == OpcaoColigada.TODOS)
                parametro = "ALL";
            else if (opcao != OpcaoColigada.NAOAUTORIZADO)
                parametro = opcao.ToString();

            return parametro;
        }

        private static string ToParameter(this TermoInternetBanking opcao)
        {
            string parametro = string.Empty;

            if (opcao.BOA && opcao.BOM)
                parametro = "ALL";
            else if (opcao.BOA)
                parametro = "BOA";
            else if (opcao.BOM)
                parametro = "BOM";
            else
                parametro = "N";

            return parametro;
        }

        public static List<DataParameter> ToDataParameter(this GerenteInfobank gerente)
        {
            var parametros = new List<DataParameter>();

            if (gerente == null)
                return parametros;

            parametros.Add(new DataParameter("@CGC_CPF", gerente.CPF));
            parametros.Add(new DataParameter("@NOME", gerente.Nome));
            parametros.Add(new DataParameter("@LOGIN", gerente.Login));
            parametros.Add(new DataParameter("@EMAIL", gerente.Email));
            parametros.Add(new DataParameter("@TIPOGERENTE", gerente.Tipo));
            parametros.Add(new DataParameter("@CODNIVEL", gerente.Nivel));
            parametros.Add(new DataParameter("@CODCCUSTO", gerente.CentroCusto));
            //parametros.Add(new DataParameter("@DATAATIVACAO", gerente.DataAtivacao));

            if (gerente.DataInativacao.HasValidDateValue())
                parametros.Add(new DataParameter("@DATAINATIVACAO", gerente.DataInativacao.Value));

            //parametros.Add(new DataParameter("@MENSAGEM", string.Empty));

            parametros.Add(new DataParameter("@NOME_USUARIO", gerente.UsuarioAlteracao));

            return parametros;
        }

        public static List<DataParameter> ToDataParameter(this Rating rating)
        {
            var parametros = new List<DataParameter>();

            if (rating == null)
                return parametros;

            var documento = rating.Pessoa is PessoaFisica ? (rating.Pessoa as PessoaFisica).CPF.Numero :
                    rating.Pessoa is PessoaJuridica ? (rating.Pessoa as PessoaJuridica).CNPJ.Numero : null;

            parametros.Add(new DataParameter("@CGC_CPF", documento));
            parametros.Add(new DataParameter("@DATAAVALIACAO", rating.Data));
            parametros.Add(new DataParameter("@ID_DOMINIO_RATING", rating.TipoRating.Identificacao));
            parametros.Add(new DataParameter("@DATAATUALIZACAO", rating.DataAtualizacao));

            parametros.Add(new DataParameter("@NOME_USUARIO", rating.Usuario));

            return parametros;
        }
    }
}
