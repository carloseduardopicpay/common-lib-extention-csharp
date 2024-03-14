using System;

namespace Domain.Entities
{
    public class PendenciaHistorico
    {
        internal PendenciaHistorico()
        {

        }

        public PendenciaHistorico(Pendencia pendencia)
        {
            this.DataInclusao = DateTime.Now;
            this.Observacao = pendencia.Observacao;
            this.Status = pendencia.Status;
            this.Usuario = pendencia.UsuarioInclusao;
        }

        public int Identificacao { get; set; }

        public DateTime DataInclusao { get; protected set; }

        public virtual ClassificacaoEntregaDocumento Status { get; set; }

        public virtual string Observacao { get; set; }

        public virtual string Usuario { get; set; }

    }
}