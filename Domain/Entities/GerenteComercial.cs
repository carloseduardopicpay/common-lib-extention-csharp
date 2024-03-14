namespace Domain.Entities
{
    public class GerenteComercial : PapelComercial
    {
        protected GerenteComercial()
        { }

        public GerenteComercial(Funcionario funcionario)
        {
            this.Funcionario = funcionario;
            this.Ativo = true;
        }
    }
}