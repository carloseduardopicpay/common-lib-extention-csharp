namespace Domain.Entities
{
    public class PessoaFisicaCadastro
    {
        internal PessoaFisicaCadastro() { }

        public PessoaFisicaCadastro(string nome, IDocumento cpf)
        {
            Nome = nome;
            CPF = cpf;
        }

        public PessoaFisicaCadastro(string nome)
        {
            Nome = nome;
        }

        public virtual string Nome { get; set; }
        public virtual IDocumento CPF { get; set; }

    }
}