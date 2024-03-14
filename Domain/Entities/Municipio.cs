namespace Domain.Entities
{
    public class Municipio 
    {
        public  int Identificacao { get; set; }

        public  string Nome { get; set; }

        public  string CodigoBNDES { get; set; }

        public  string CodigoCETIP { get; set; }

        public  UnidadeFederativa UF { get; set; }
    }

}