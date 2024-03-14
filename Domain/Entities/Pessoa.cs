using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Domain.Entities
{
    public class Pessoa
    {
        public List<Endereco>? Enderecos { get; set; }
        public List<Contato>? Contatos { get; set; }
        public List<Referencia>? Referencias { get; set; }
        public List<ClassificacaoPessoa>? ClassificacaoPessoas { get; set; }
        public DadoPatrimonial? DadosPatrimoniais { get; set; }
        public List<Procurador>? Procuradores { get; set; }
        public List<Vinculo>? GrupoParticipacao { get; set; }
        public List<Restricao>? Restricoes { get; set; }

        public List<DadoEconomicoPessoaJuridica>? DadosEconomicos { get; set; }

        public List<Rating>? Ratings { get; set; }

        public int Identificacao { get; protected set; }
        public string? CodigoExterno { get; set; }
        public INomePessoa? Nome { get; set; }
        public string? NomeCompleto { get; set; }

        public string NomeAbreviado
        {
            get
            {
                if (this.Nome != null)
                    return this.Nome.NomeAbreviado;
                else
                    return string.Empty;
            }
            set
            {
            }
        }

        public DateTime? DataSituacaoCadastral { get; set; }
        public string? NomeCheque { get; set; }
        public string? NomeCartao { get; set; }
        public ICollection<ClassificacaoPessoa>? Classificacoes { get; set; }
        public ICollection<Documento>? Documentos { get; set; }
        public IList<SetorAtividade>? CNAESecundario { get; set; }
        public string Origem { get; set; }
        public DateTime? DataRenovacao { get; set; }
        public string WebSite { get; set; }
        public string? PermiteConsultaBACEN_NCR { get; set; }
        public bool PermiteConsultaBACEN_PCAM { get; set; }
        public string? CodigoOperacionalCVM { get; set; }
        public DateTime? DataInclusao { get; protected set; }
        public DateTime? DataAtualizacao { get; set; }
        public string? UsuarioInclusao { get; set; }
        public string? UsuarioAlteracao { get; set; }
        public bool ExigeFeitoConferido
        {
            get
            {
                return this != null
                    && this.Classificacoes != null
                    && this.Classificacoes.Where(x => x.ExigeFeitoConferido == true).Count() > 0;
            }
            protected set
            {
            }
        }

        public List<RelacionamentoComCliente>? RelacionamentoComCliente { get; set; }
        public Officer OfficerResponsavel { get; set; }
        public GerenteComercial Gerente { get; set; }
        public GerenteComercial GerenteAnterior { get; set; }
        public AgenteDigital AgenteDigital { get; set; }
        public string Campanha { get; set; }
        public bool ClienteFEPWeb { get; set; }
        public bool SacadoFEPWeb { get; set; }
        public bool PEPInformado { get; set; }
        public bool FATCAInformado { get; set; }
        public Dominio? OrigemCadastro { get; set; }
    }

    public interface INomePessoa : IComparable, IComparable<string>, IEquatable<string>
    {
        string NomeCompleto { get; }
        string NomeAbreviado { get; set; }
        bool StartsWith(string value);
        string ToUpper();
        string ToUpper(CultureInfo culture);
        string ToUpperInvariant();
    }
}
