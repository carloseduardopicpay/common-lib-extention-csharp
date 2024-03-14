using System;

namespace Domain.Entities
{
    public abstract class DocumentoIdentificacao
    {
        public virtual int Identificacao { get; set; }

        protected virtual TipoIdentificacao Tipo { get; set; }

        public virtual string Numero { get; set; }

        public virtual DateTime? DataEmissao { get; set; }

        public virtual OrgaoEmissor OrgaoEmissor { get; set; }

        public virtual UnidadeFederativa UF { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual string Ativo { get; set; }

        public virtual bool DocumentoConjuge { get; set; }

        public virtual bool ConcatenaNumeroComUF
        {
            get
            {
                return false;
            }
        }
    }
}