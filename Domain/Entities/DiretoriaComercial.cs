using System.Collections.Generic;

namespace Domain.Entities
{
    public class DiretoriaComercial : Estrutura
    {
        protected DiretoriaComercial()
        { }

        public DiretoriaComercial(string nome)
            : base(nome)
        {

        }

        public virtual ICollection<Regional> Regionais
        {
            get;
            protected set;
        }
    }
}