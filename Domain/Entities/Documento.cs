using System.Collections.Generic;
using System;
using System.Linq;

namespace Domain.Entities
{
    public class Documento
    {
        protected Documento()
        {
            this.Historico = new List<DocumentoHistorico>();
        }

        public Documento(TipoDocumento tipoDocumento, string usuarioInclusao) : this()
        {
            this.Tipo = tipoDocumento;
            this.DataInclusao = DateTime.Now;
        }

        public virtual int Identificacao { get; protected set; }

        public virtual string Nome
        {
            get
            {
                string desc = string.Empty;

                if (this != null && this.Tipo != null)
                    desc = this.Tipo.Descricao;

                return desc;
            }
            protected set
            {
            }
        }

        public virtual TipoDocumento Tipo { get; protected set; }

        public virtual DateTime DataInclusao { get; protected set; }

        public virtual DateTime? DataValidade { get; set; }

        public virtual ClassificacaoEntregaDocumento Status { get; protected set; }

        public virtual string Observacao { get; set; }

        private string _usuariobaixa;
        public virtual string UsuarioBaixa
        {
            get
            {
                if (this.Status != null && !this.Status.GeraPendencia)
                    return _usuariobaixa;
                else
                    return string.Empty;
            }
            set
            {
                _usuariobaixa = value;
            }
        }

        public virtual Pendencia Pendencia { get; protected set; }

        public virtual IList<DocumentoHistorico> Historico { get; protected set; }

        public virtual void DefinirPendencia(Pendencia pendencia, string identificacaoUsuario)
        {
            if (pendencia == null)
                throw new ArgumentNullException("Pendencia", "Pendência deve ser informada.");

            throw new NotImplementedException();

        }

        public virtual void AlterarStatus(DocumentoHistorico historico)
        {
            if (historico == null)
                throw new ArgumentNullException("historico", "Impossível adicionar um histórico nulo ao documento.");
            else if (string.IsNullOrEmpty(historico.Usuario))
                throw new ArgumentException("Para adicionar um histórico ao documento é obrigatório informar o usuário.");

            if (this.Historico == null)
                this.Historico = new List<DocumentoHistorico>();

            var ultimoHistorico = this.Historico.OrderByDescending(x => x.DataInclusao).FirstOrDefault();

            if (ultimoHistorico != null && ultimoHistorico.Status != null)
            {
                // Se status anterior gerava pendência, salva quando e por quem foi feita a alteração.
                if (ultimoHistorico.Status.GeraPendencia)
                {
                    ultimoHistorico.DataBaixa = DateTime.Today;
                    ultimoHistorico.Usuario = historico.Usuario;
                }
                // Se o status anterior não gerava pendência e só tem um, inicializa o histórico com a pendência sendo incluída.
                else if (this.Historico.Count <= 1)
                {
                    this.Historico.Remove(ultimoHistorico);
                }
            }

            this.Historico.Add(historico);

            ultimoHistorico = this.Historico.OrderByDescending(x => x.DataInclusao).FirstOrDefault();

            if (ultimoHistorico != null && ultimoHistorico.Status != null)
            {
                if (ultimoHistorico.Status.GeraPendencia)
                {
                    this.UsuarioBaixa = string.Empty;
                    ultimoHistorico.DataBaixa = null;
                }
                else
                {
                    this.UsuarioBaixa = ultimoHistorico.Usuario = historico.Usuario;
                    ultimoHistorico.DataBaixa = DateTime.Today;
                }
            }

            this.Status = ultimoHistorico.Status;

        }

        public virtual void AcertarReferenciaHistorico()
        {
            if (this.Historico == null || this.Historico.Count == 0)
                return;

            foreach (var historico in this.Historico.Where(x => x.Documento == null))
            {
                historico.Documento = this;
            }
        }
    }
}