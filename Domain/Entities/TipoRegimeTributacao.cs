namespace Domain.Entities
{
    public class TipoRegimeTributacao
    {
        public virtual int Identificacao { get; set; }

        public virtual string Descricao { get; set; }

        public virtual bool Ativo { get; set; }
    }
}