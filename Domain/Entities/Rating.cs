using System;

namespace Domain.Entities
{
    public class Rating
    {
        protected Rating()
        { }

        public Rating(Pessoa pessoa, DateTime data, Dominio tipoRating, string usuario)
        {
            this.Pessoa = pessoa;
            this.Data = data;
            this.TipoRating = tipoRating;
            this.Usuario = usuario;
        }

        public virtual int Identificacao { get; set; }

        public virtual Pessoa Pessoa { get; set; }

        public virtual DateTime Data { get; set; }

        public virtual Dominio TipoRating { get; set; }

        public virtual DateTime DataInclusao { get; protected set; }

        public virtual DateTime DataAtualizacao { get; set; }

        public virtual string Usuario { get; set; }
    }
}