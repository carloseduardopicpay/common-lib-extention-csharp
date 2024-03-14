namespace Domain.Entities
{
    public class FazendaProdutorRural
    {
        public virtual int Identificacao { get; set; }

        public virtual string Fazenda { get; set; }

        public virtual Dominio Condicao { get; set; }

        public virtual PessoaFisica Pessoa { get; set; }
    }
}