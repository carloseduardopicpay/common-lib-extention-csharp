using System;

namespace Domain.Entities
{
    public class Passaporte : DocumentoIdentificacao
    {
        public virtual DateTime? DataValidade { get; set; }
    }
}
