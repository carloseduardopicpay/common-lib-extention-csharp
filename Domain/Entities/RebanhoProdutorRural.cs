using System;

namespace Domain.Entities
{
    public class RebanhoProdutorRural
    {
        public virtual int Identificacao { get; set; }
        public virtual string Categoria { get; set; }
        public virtual int Qtde { get; set; }
        public virtual DateTime? DataAbate { get; set; }
        public virtual decimal ValorMedioRegional { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual PessoaFisica Pessoa { get; set; }
    }
}