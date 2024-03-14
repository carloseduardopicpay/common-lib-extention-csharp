namespace Domain.Entities
{
    public class CapitalSocial
    {
        public CapitalSocial(decimal valor, Dominio tipo)
        {
            this.Valor = valor;
            this.Tipo = tipo;
        }

        public CapitalSocial() { }

        public virtual decimal Valor { get; set; }

        // Tipo CapitalSocial como Dominio
        public virtual Dominio Tipo { get; set; }

        public virtual Pais PaisOrigem { get; set; }

        public virtual bool CapitalAberto { get; set; }

        public virtual decimal ValorCapitalAberto { get; set; }
    }
}