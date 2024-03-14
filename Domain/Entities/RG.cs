namespace Domain.Entities
{
    public class RG : DocumentoIdentificacao
    {
        private static string DescricaoTipo
        {
            get
            {
                return "RG";
            }
        }

        public override bool ConcatenaNumeroComUF
        {
            get
            {
                return true;
            }
        }

    }
}
