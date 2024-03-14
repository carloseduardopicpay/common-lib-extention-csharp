namespace Domain.Entities
{
    public class Banco 
    {
        public virtual int Identificacao { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Sigla { get; set; }

        public virtual bool Ativo { get; set; }
    }
}