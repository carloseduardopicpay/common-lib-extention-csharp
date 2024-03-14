using System;

namespace Domain.Entities
{
    public class PEP
    {
        public  int Identificacao { get; set; }

        // Pessoa à quem possui relacionamento.
        public  Pessoa Pessoa { get; set; }

        // Orgão ou nome da pessoa politicamente exposta.
        public  string OrgaoOuNomePessoa { get; set; }

        // CPF/CNPJ da pessoa politicamente exposta.
        public  string Documento { get; set; }

        // Cargo que ocupa ou Grau de parentesco da pessoa politicamente exposta.
        public  string CargoOuParentesco { get; set; }

        // Data de inicio do mandato.
        public  DateTime DataInicioMandato { get; set; }

        // Data de término do mandato.
        public  DateTime DataTerminoMandato { get; set; }

    }
}