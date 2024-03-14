namespace Domain.Entities
{
    public class Dominio : IDominio
    {
        public Dominio(string descricao)
        {
            Descricao = descricao;
        }

        public Dominio() { }

        public  int Identificacao { get; set; }
        public  string Descricao { get; set; }
        public  bool Ativo { get; set; }
        public  GrupoDominio Grupo { get; protected set; }

        public  Dominio AlterarGrupo(GrupoDominio grupo)
        {
            if (grupo == null)
                return this;

            Grupo = grupo;

            return this;
        }

        public override string ToString()
        {
            return this.Descricao;
        }
    }

    public interface IDominio
    {
        int Identificacao { get; set; }
    }
}