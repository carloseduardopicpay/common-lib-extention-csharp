using System;

namespace Domain.Entities
{
    public class SetorAtividade 
    {
        public  int Identificacao { get; set; }

        public  string Codigo { get; set; }

        public  string Descricao { get; set; }

        public  Boolean? IsencaoIR { get; set; }

        public  Boolean? IsencaoIOF { get; set; }

        public  string Versao { get; set; }

        public  DateTime DataInclusao { get; set; }

        public  DateTime DataAlteracao { get; set; }

        public  string? Ativo { get; set; }

        public  SubGrupoSetorAtividade ? SubGrupoSetorAtividade { get; set; }

        public  TipoInstituicao ? TipoInstituicao { get; set; }
    }
}