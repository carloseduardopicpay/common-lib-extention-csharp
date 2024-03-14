using System;

namespace Domain.Entities
{
    public abstract class PapelComercial
    {
        public virtual int Id { get; protected set; }

        public virtual Funcionario? Funcionario { get; protected set; }

        public virtual string Nome
        {
            get
            {
                if (this != null && this.Funcionario != null)
                    return this.Funcionario.Nome;
                else
                    return string.Empty;
            }
        }

        public virtual bool Ativo { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                const int HashingBase = (int)2166136261;
                const int HashingMultiplier = 16777619;

                int hash = HashingBase;
                hash = (hash * HashingMultiplier) ^ (!Object.ReferenceEquals(null, Id) ? Id.GetHashCode() : 0);
                return hash;
            }
        }

        public override bool Equals(object value)
        {
            return this.Equals(value as PapelComercial);
        }

        public virtual bool Equals(PapelComercial value)
        {
            if (Object.ReferenceEquals(null, value))
                return false;

            if (value.GetType() != this.GetType())
                return false;

            if (Object.ReferenceEquals(this, value))
                return true;

            return int.Equals(this.Id, value.Id);

        }
    }
}