namespace Domain.Entities
{
    public class Pais
    {
        public virtual int Identificacao { get; set; }

        public virtual string Nome { get; set; }

        public virtual string Sigla { get; set; }

        public virtual string CodigoBancoCentralBrasil { get; set; }

        public virtual bool ParaisoFiscal { get; set; }

        public virtual Dominio Nacionalidade { get; set; }

        public virtual string CodigoCETIP { get; set; }

        public virtual int? IdentificacaoNacionalidade { get; set; }
    }
}