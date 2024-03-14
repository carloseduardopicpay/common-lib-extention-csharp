using System.Collections.Generic;
using System;

namespace Domain.Entities
{
    public class Pendencia
    {
        protected Pendencia()
            : base()
        {
            this.DataInclusao = DateTime.Now;
        }

        public Pendencia(string usuarioInclusao)
            : this()
        {
            this.UsuarioInclusao = usuarioInclusao;
            this.DataAtualizacao = DateTime.Now;
            this.Historico = new List<PendenciaHistorico>();
            this.Autorizantes = new List<Funcionario>();
        }

        public virtual int Identificacao { get; protected set; }

        public virtual string Observacao { get; set; }

        public virtual string UsuarioBaixa { get; set; }

        public virtual string UsuarioInclusao { get; protected set; }

        public virtual IList<Funcionario> Autorizantes { get; protected set; }

        public virtual ClassificacaoEntregaDocumento Status { get; set; }

        public virtual DateTime DataAtualizacao { get; set; }

        public virtual DateTime DataInclusao { get; protected set; }

        public virtual IList<PendenciaHistorico> Historico { get; protected set; }

    }
}