using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace Domain.Entities
{
    public class GrupoDominio : IDominio
    {
        protected GrupoDominio() { }

        public GrupoDominio(string descricao, IEnumerable<Dominio> valores)
        {
            if (valores == null || valores.Count() == 0)
                throw new BusinessException("Ao menos um valor é necessário para se criar um grupo de domínios.");

            Descricao = descricao;
            Valores = valores;
        }

        public virtual int Identificacao { get; set; }
        public virtual string Descricao { get; set; }

        private List<Dominio> _valores;
        public virtual IEnumerable<Dominio> Valores
        {
            get { return _valores.AsEnumerable(); }
            protected set
            {
                _valores = value.ToList();
                _valores.ForEach(valor =>
                {
                    Classificar(valor);
                });
            }
        }

        public virtual GrupoDominio IncluirDominio(Dominio dominio)
        {
            if (dominio == null)
                throw new BusinessException("Oops, você está tentando adicionar um domínio nulo.");

            if (_valores.Any(valor => valor.Equals(dominio)))
                throw new BusinessException("O domínio já existe para o Grupo selecionado.");

            Classificar(dominio);

            _valores.Add(dominio);

            return this;
        }

        private void Classificar(Dominio dominio)
        {
            dominio.AlterarGrupo(this);
        }
    }
}