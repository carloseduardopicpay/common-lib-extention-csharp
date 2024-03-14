using System.ComponentModel;

namespace Infra.CrossCutting.Enums
{
    public class Enums
    {
        public enum log_levels
        {

            [Description("INFO")]
            INFO,
            [Description("WARN")]
            WARN,
            [Description("ERRO")]
            ERRO
        }

        public enum OpcaoColigada
        {
            NAOAUTORIZADO,
            BOM,
            BOA,
            TODOS
        }
    }
}
