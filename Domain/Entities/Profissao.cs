using System;

namespace Domain.Entities
{
    public class Profissao
    {
        public virtual int Identificacao { get; set; }

        public virtual int Codigo { get; set; }

        public virtual string Descricao { get; set; }

        public virtual bool ProfissaoRural { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual DateTime DataInclusao { get; set; }

        public virtual DateTime DataAtualizacao { get; set; }

    }
}