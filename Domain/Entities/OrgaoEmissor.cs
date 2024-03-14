using System;

namespace Domain.Entities
{
    public class OrgaoEmissor : IDominio
    {
        public OrgaoEmissor() { }

        public OrgaoEmissor(int identificacao)
        {
            this.Identificacao = identificacao;
        }

        public virtual int Identificacao { get; set; }

        public virtual string Sigla { get; set; }

        public virtual string Descricao { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual DateTime DataInclusao { get; set; }

        public virtual DateTime DataAtualizacao { get; set; }

    }
}