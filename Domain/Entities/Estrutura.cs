namespace Domain.Entities
{
    public abstract class Estrutura
    {
        protected Estrutura()
        { }

        public Estrutura(string nome)
        {
            this.Nome = nome;
        }

        public virtual int Identificacao { get; set; }

        private string _nome;
        public virtual string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value.ToUpper();
            }
        }
    }
}