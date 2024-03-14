namespace Domain.Entities
{
    public class PapelComercialEstrutura
    {
        protected PapelComercialEstrutura() { }
        public virtual int Identificacao { get; set; }
        public virtual Estrutura Estrutura { get; set; }
        public virtual PapelComercial Colaborador { get; set; }

        public static PapelComercialEstrutura Novo(Estrutura estrutura, PapelComercial papel)
        {
            return new PapelComercialEstrutura
            {
                Estrutura = estrutura,
                Colaborador = papel
            };
        }
    }
}