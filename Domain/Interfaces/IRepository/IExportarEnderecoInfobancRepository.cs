using System.Threading.Tasks;

namespace Domain.Interfaces.IRepository
{
    public interface IExportarEnderecoInfobancRepository
    {
        Task<string> ExportarEndereco
            (string CNPJ, string connectionString);
    }
}
