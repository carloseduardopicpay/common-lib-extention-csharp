namespace Domain.Entities
{
    public class AgenteDigital : PapelComercial
    {
        protected AgenteDigital()
        { }

        public AgenteDigital(Funcionario funcionario)
        {
            this.Funcionario = funcionario;
            this.Ativo = true;
        }
    }
}