namespace Domain.Entities
{
    public class SituacaoLegal
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Observacao { get; set; }
        public virtual int IdadeInicial { get; set; }
        public virtual int IdadeFinal { get; set; }
    }
}