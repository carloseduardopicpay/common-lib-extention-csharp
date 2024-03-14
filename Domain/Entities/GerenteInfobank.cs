using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GerenteInfobank
    {
        public virtual string Codigo { get; set; }

        public virtual string CPF { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Login { get; set; }
        public virtual string Email { get; set; }
        public virtual string Tipo { get; set; }
        public virtual string Nivel { get; set; }
        public virtual string CentroCusto { get; set; }
        public virtual DateTime? DataInativacao { get; set; }

        public virtual string UsuarioAlteracao { get; set; }
    }
}
