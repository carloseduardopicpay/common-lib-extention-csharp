using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Officer : PapelComercial
    {
        protected Officer()
        {

        }

        public Officer(Funcionario funcionario)
        {
            this.Funcionario = funcionario;
        }

        public virtual IList<Estrutura> EstruturaComercial { get; set; }

        public virtual string Nome
        {
            get
            {
                return this.Funcionario.Nome;
            }
        }

        public virtual GerenteComercial Gerente
        {
            get
            {
                GerenteComercial gerente = null;
                var unidade = this.EstruturaComercial.OfType<Unidade>().FirstOrDefault();

                if (unidade != null && unidade.Pai != null && unidade.Pai.Pai != null)
                {
                    gerente = unidade.Pai.Pai.Responsavel;
                }

                return gerente;
            }
            protected set
            { }
        }

    }
}