using System;

namespace Domain.Entities
{
    public class DocumentoHistorico
    {
        internal DocumentoHistorico()
        {
            this.DataInclusao = DateTime.Now;
        }

        public DocumentoHistorico(Documento documento, ClassificacaoEntregaDocumento status, string identificacaoUsuario)
            : this()
        {
            if (string.IsNullOrEmpty(identificacaoUsuario))
                throw new ArgumentException("Para criar um histórico do documento é obrigatório informar o usuário.");
            else if (documento == null)
                throw new ArgumentNullException("O documento deve ser informado");
            else if (status == null)
                throw new ArgumentNullException("Status inválido para o documento");

            this.Documento = documento;
            this.Status = status;
            this.DataValidade = documento.DataValidade;
            this.Observacao = documento.Observacao;
            this.Usuario = identificacaoUsuario;
        }

        public  Documento Documento { get; protected internal set; }

        public  int Identificacao { get; protected set; }

        public  DateTime DataInclusao { get; protected set; }

        public  DateTime? DataEntrada { get; set; }

        public  DateTime? DataValidade { get; protected set; }

        public  DateTime? DataBaixa { get; set; }

        public  ClassificacaoEntregaDocumento Status { get; protected internal set; }

        public  Funcionario PrimeiroAutorizante { get; set; }

        public  Funcionario SegundoAutorizante { get; set; }

        public  string Observacao { get; set; }

        public  string Usuario { get; protected internal set; }

        public  bool Renovacao { get; set; }

        public  int? IdentificacaoClienteRelacionado { get; set; }
    }
}