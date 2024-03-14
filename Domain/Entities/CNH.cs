using System;

namespace Domain.Entities
{
    public class CNH : DocumentoIdentificacao
    {
        public virtual Dominio Categoria { get; set; }
        public virtual DateTime? DataPrimeiraHabilitacao { get; set; }
        public virtual DateTime? DataValidade { get; set; }
    }
}
