using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace Domain.Entities
{
    public class Unidade : Estrutura
    {
        protected Unidade()
        { }

        public Unidade(string nome)
            : base(nome)
        { }

        private IList<PapelComercialEstrutura> _colaboradores;
        public virtual IEnumerable<PapelComercialEstrutura> Colaboradores
        {
            get { return _colaboradores; }
            protected set { _colaboradores = value.ToList(); }
        }

     
      

        private SubRegional _pai;
        public virtual SubRegional Pai
        {
            get { return _pai; }
            set
            {
                if (value == null)
                    throw new BusinessException("Uma Unidade sempre deve estar abaixo de uma SubRegional.");

                _pai = value;
            }
        }

       
       
        private Unidade AdicionarPapel(PapelComercialEstrutura papelEmUmaEstrutura)
        {
            if (_colaboradores == null)
                _colaboradores = new List<PapelComercialEstrutura>();

            _colaboradores.Add(papelEmUmaEstrutura);

            return this;
        }
    }
}