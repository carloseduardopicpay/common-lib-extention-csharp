using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;

namespace Application.Services
{
    public class InfobancService : IInfobancService
    {
        private readonly IInfobankrepository _infobankrepository;
        public InfobancService( IInfobankrepository infobankrepository)
        {
            _infobankrepository = infobankrepository;
        }

        public string ExportarInfobancService(string CNPJ)
        {
             return _infobankrepository.Exportarparainfobanc(CNPJ).Result;   

        }

       
    }
}
