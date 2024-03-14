using System;

namespace Domain.Entities
{
    public class ClassificacaoPessoa : IEquatable<ClassificacaoPessoa>
    {
        protected ClassificacaoPessoa()
        { }

        public virtual int Identificacao { get; set; }

        public virtual string Descricao
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public virtual Pessoa Pessoa { get; set; }

        public virtual bool ExigeFeitoConferido { get; protected set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as ClassificacaoPessoa);
        }

        public virtual bool Equals(ClassificacaoPessoa other)
        {
            if (other == null)
                return false;

            bool isSame = other.Pessoa.Equals(this.Pessoa) && other.GetType().Equals(this.GetType());

            if (this.Identificacao != 0 && other.Identificacao != 0)
            {
                isSame = isSame && this.Identificacao.Equals(other.Identificacao);
            }

            return isSame;

        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual string Nome
        {
            get
            {
                return Pessoa != null ? Pessoa.Nome.NomeAbreviado : string.Empty;

            }
        }
    }
}