namespace Domain.Entities
{
    public class Escolaridade
    {
        public virtual Dominio TipoEscolaridade { get; set; }
        public virtual Dominio SituacaoEscolaridade { get; set; }
        public virtual string Curso { get; set; }
    }
}