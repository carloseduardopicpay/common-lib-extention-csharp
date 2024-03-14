namespace Domain.Entities
{
    public class Dependente : PessoaFisicaCadastro
    {
        public virtual int Identificacao { get; set; }
        public virtual Dominio TipoDependente { get; set; }

        public virtual PessoaFisica Responsavel { get; set; }

        public virtual string Ativo { get; set; }
    }
}