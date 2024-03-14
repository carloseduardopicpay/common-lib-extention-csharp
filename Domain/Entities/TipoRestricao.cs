namespace Domain.Entities
{
    public class TipoRestricao
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool OrigemPpe { get; set; }
    }
}