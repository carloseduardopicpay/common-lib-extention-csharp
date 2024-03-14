namespace Domain.Entities
{
    public class Periodicidade 
    {
        public virtual int Identificacao { get; set; }

        public virtual string Descricao { get; set; }

        public virtual int Dias { get; set; }

        public virtual string Codigo { get; set; }

    }
}