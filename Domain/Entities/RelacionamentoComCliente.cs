namespace Domain.Entities
{
    public class RelacionamentoComCliente
    {
        public  int Identificacao { get; set; }

        private ClassificacaoPessoa _classificacao;
        public  ClassificacaoPessoa Classificacao
        {
            get
            {
                return _classificacao;
            }
            set
            {
                this._classificacao = value;

                if (value != null)
                {
                    this.ClassificacaoRelacionada = value.Descricao;

                    if (value.Pessoa != null)
                        this.Pessoa = value.Pessoa;
                }
                else
                {
                    this.Pessoa = null;
                }
            }
        }

        public  Cliente Cliente { get; set; }

        public  Pessoa Pessoa { get; protected set; }

        // TODO: Remover propreidade após correção do problema do temporário. 
        // Propriedade criada para contornar problema da edição no temporário onde, ao recuperar, não retorna a classificação associada.
        public  string ClassificacaoRelacionada { get; set; }
    }
}