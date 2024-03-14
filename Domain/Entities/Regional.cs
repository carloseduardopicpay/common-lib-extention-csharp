using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace Domain.Entities
{
    public class Regional : Estrutura
    {
        protected Regional()
        { }

        public Regional(string nome, GerenteComercial responsavel)
            : base(nome)
        {
            if (responsavel == null)
                throw new BusinessException("Oops, parece que você está tentando criar uma Regional sem um Gerente Comercial.");

            this.Responsavel = responsavel;
            this.Assistentes = new List<Officer>();
            this.SubRegionais = new List<SubRegional>();
            this.Unidades = new List<Unidade>();
        }

        public virtual GerenteComercial Responsavel
        {
            get;
            protected set;
        }

        private List<Officer> _assistentes;
        public virtual IEnumerable<Officer> Assistentes
        {
            get
            {
                return _assistentes;
            }
            protected set
            {
                _assistentes = value.ToList();
            }
        }

        public virtual void AdicionarAssistente(Officer assistente)
        {
            if (Assistentes.Contains(assistente))
                throw new BusinessException("Assistente " + assistente.Funcionario.Nome + " já cadastrado.");

            _assistentes.Add(assistente);
        }

        public virtual ICollection<SubRegional> SubRegionais
        {
            get;
            protected set;
        }

        public virtual ICollection<Unidade> Unidades
        {
            get;
            protected set;
        }

        private DiretoriaComercial _pai;
        public virtual DiretoriaComercial Pai
        {
            get { return _pai; }
            set
            {
                if (value == null)
                    throw new BusinessException("Uma Regional sempre deve estar abaixo de uma Diretoria Comercial.");

                _pai = value;
            }
        }
    }
}