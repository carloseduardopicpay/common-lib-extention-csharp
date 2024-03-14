using System.Linq;
using System.Text;
using Volo.Abp;

namespace Domain.Entities
{
    public class Procurador : ClassificacaoPessoa
    {
        protected Procurador()
        {
        }

        public Procurador(Pessoa pessoa)
        {
            if (pessoa == null)
                throw new BusinessException("Uma pessoa deve ser fornecida.");

            if (pessoa.Classificacoes != null && pessoa.Classificacoes.OfType<Procurador>().FirstOrDefault() != null)
                throw new BusinessException("O cadastro informado já é um procurador!");

            StringBuilder camposObrigatorios = new StringBuilder();

            if (string.IsNullOrWhiteSpace(pessoa.NomeCompleto))
                camposObrigatorios.AppendLine("Nome");


            if (camposObrigatorios.Length > 0)
            {
                string _menssagem = string.Format("Para criar um {0} os devidos campos são obrigatórios:\n", this.GetType().Name);

                throw new BusinessException(_menssagem + camposObrigatorios.ToString());
            }

            this.Pessoa = pessoa;
            Pessoa.Classificacoes.Add(this);
        }

        public virtual string Ativo { get; set; }
    }
}