using System.Collections.Generic;

namespace Domain.Entities
{
    public class Endereco
    {
        public Endereco()
        {
            this.Telefones = new List<Telefone>();
            this.Ativo = "1";
        }

        public  int Identificacao { get; set; }

        public  Dominio Tipo { get; set; }

        public  string ObservacaoTipo { get; set; }

        public  string Logradouro { get; set; }

        public  string Numero { get; set; }

        public  string Complemento { get; set; }

        public  string Bairro { get; set; }

        public  string CEP { get; set; }

        public  Municipio Municipio { get; set; }

        public  string CaixaPostal { get; set; }

        public  Dominio TipoPosse { get; set; }

        public  Dominio TipoLogradouro { get; set; }

        public  string Roteiro { get; set; }

        public  bool IsEnderecoCorrespondencia { get; set; }

        public  string ResideDesdeAno { get; set; }

        public  ICollection<Telefone> Telefones { get; set; }

        public  string Ativo { get; set; }

    }
}
