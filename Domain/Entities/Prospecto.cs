using System.Linq;
using System.Text;
using Volo.Abp;

namespace Domain.Entities
{
    public class Prospecto : ClassificacaoPessoa
    {
        protected Prospecto()
        {
        }

        public Prospecto(Pessoa pessoa)
        {
            string validacao = string.Empty;

            if (PodeClassificar(pessoa, out validacao))
            {
                this.Pessoa = pessoa;
                Pessoa.Classificacoes.Add(this);
            }
            else
            {
                throw new BusinessException(validacao);
            }
        }

        public virtual Officer OfficerResponsavel
        {
            get
            {
                if (this != null && this.Pessoa != null && this.Pessoa.OfficerResponsavel != null)
                {
                    return this.Pessoa.OfficerResponsavel;
                }
                else
                {
                    return null;
                }
            }
            protected set
            {
            }
        }

        public static bool PodeClassificar(Pessoa pessoa, out string motivoRejeicao)
        {
            StringBuilder camposObrigatorios = new StringBuilder();

            if (pessoa == null)
            {
                motivoRejeicao = "Pessoa não informada na validação!";
                return false;
            }

            if (pessoa.Classificacoes.OfType<Prospecto>().FirstOrDefault() != null)
            {
                motivoRejeicao = "O cadastro informado já é um prospecto!";
                return false;
            }

            if (pessoa.Nome == null || string.IsNullOrWhiteSpace(pessoa.Nome.ToString()))
                camposObrigatorios.AppendLine("Nome");

            if (pessoa is PessoaFisica)
            {
                var pf = (PessoaFisica)pessoa;

                if (pf.CPF == null)
                    camposObrigatorios.AppendLine("CPF");

                if (pf.PaisNascimento == null)
                    camposObrigatorios.AppendLine("País de Nascimento");

                if (pf.DocumentosIdentificacao == null || pf.DocumentosIdentificacao.Count == 0)
                    camposObrigatorios.AppendLine("Documento de Identificação");
            }
            if (pessoa is PessoaJuridica)
            {
                if (((PessoaJuridica)pessoa).CNPJ == null)
                    camposObrigatorios.AppendLine("CNPJ");
            }

            if (!string.IsNullOrWhiteSpace(camposObrigatorios.ToString()))
            {
                camposObrigatorios.Insert(0, "Dados obrigatórios não informados:\n");
            }

            motivoRejeicao = camposObrigatorios.ToString();
            return string.IsNullOrWhiteSpace(motivoRejeicao);
        }

    }
}
