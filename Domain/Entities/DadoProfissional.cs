using System;

namespace Domain.Entities
{
        public class DadoProfissional
        {
            public virtual NaturezaOcupacao NaturezaOcupacao { get; set; }

            public virtual Profissao Profissao { get; set; }

            public virtual Empresa Empresa { get; set; }

            public virtual DateTime? DataAdmissao { get; set; }

            public virtual bool AposentadoOuPensionista { get; set; }

            public virtual string NumeroCartaoINSS { get; set; }

            public virtual Dominio Cargo { get; set; }

            public virtual bool? ProdutorRuralRelacionaJBS { get; set; }

            public virtual string ProdutorRuralRelacionaJBSContato { get; set; }

            public virtual Dominio Ocupacao { get; set; }

        }
    }
