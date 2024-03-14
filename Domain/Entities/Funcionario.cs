using System;

namespace Domain.Entities
{
    public class Funcionario : IEquatable<Funcionario>
    {
        public virtual string Identificacao { get; set; }

        public virtual string Matricula { get; set; }

        public virtual string Nome { get; set; }

        public virtual bool Ativo { get; set; }

        public virtual bool Equals(Funcionario other)
        {
            return this.Identificacao == other.Identificacao;
        }
    }
}