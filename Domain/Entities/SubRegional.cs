using System.Collections.Generic;
using Volo.Abp;

namespace Domain.Entities
{
    public class SubRegional : Estrutura
    {
        protected SubRegional()
        { }

        public SubRegional(string nome, Officer responsavel) : base(nome)
        {
            this.Responsavel = responsavel;
            this.Unidades = new List<Unidade>();
        }

        public virtual Officer Responsavel
        {
            get;
            set;
        }

        public virtual ICollection<Unidade> Unidades
        {
            get;
            set;
        }

        private Regional _pai;
        public virtual Regional Pai
        {
            get { return _pai; }
            set
            {
                if (value == null)
                    throw new BusinessException("Uma SubRegional sempre deve estar abaixo de uma Regional.");

                _pai = value;
            }
        }
    }
}