namespace Domain.Entities
{
    public class Empresa
    {
        public Empresa()
        {

        }

        public Empresa(CNPJ cnpj, string razaoSocial)
        {
            this.CNPJ = cnpj;
            this.Nome = razaoSocial;
        }

        public Empresa(string cnpj, string razaoSocial)
            : this(new CNPJ(cnpj), razaoSocial)
        {
        }

        public virtual CNPJ CNPJ { get; set; }

        public virtual string Nome { get; set; }

        public virtual decimal CapitalSocial { get; set; }

    }
}