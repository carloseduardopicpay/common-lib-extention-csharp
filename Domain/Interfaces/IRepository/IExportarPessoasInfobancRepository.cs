using System.Threading.Tasks;

namespace Domain.Interfaces.IRepository
{
    public interface IExportarPessoasInfobancRepository
    {
        Task<string> ExportarPessoas(string CNPJ, string connectionString);
    }
}
