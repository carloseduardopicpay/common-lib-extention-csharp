namespace Domain.Entities
{
    public class TipoInformacaoRenda : Tipo
    {
        public virtual string Mnemonico { get; set; }

        public virtual bool CompoeRenda { get; set; }

    }
}