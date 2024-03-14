using System.Threading.Tasks;

namespace Domain.Interfaces.IRepository
{
    public interface IInfobankrepository
    {
        Task<string> Exportarparainfobanc(string cnpj);
     }
}
