namespace Domain.Entities
{
    public class UnidadeFederativa
    {
        public  int Identificacao { get; set; }

        public  string Nome { get; set; }

        public  string Sigla { get; set; }

        public  string CodigoIBGE { get; set; }

        public  Pais Pais { get; set; }

    }
}