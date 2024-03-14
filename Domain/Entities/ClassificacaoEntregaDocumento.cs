namespace Domain.Entities
{
    public class ClassificacaoEntregaDocumento 
    {
        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual bool GeraPendencia { get; set; }
        public virtual bool Ativo { get; set; }
    }
}