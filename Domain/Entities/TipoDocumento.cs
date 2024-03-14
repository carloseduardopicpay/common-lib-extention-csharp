namespace Domain.Entities
{
    public class TipoDocumento : Tipo
    {
        public virtual string Codigo { get; set; }

        public virtual bool Ativo { get; set; }
    }

    public abstract class Tipo 
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
    }
}