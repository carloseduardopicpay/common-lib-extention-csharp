namespace Domain.Entities
{
    public abstract class TipoAtividade 
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool NivelConsolidador { get; set; }
        public virtual string TipoSetor { get; set; }
    }
}