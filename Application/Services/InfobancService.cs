using AutoMapper;
using Domain.Entities;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;

namespace Application.Services
{
    public class InfobancService : IInfobancService
    {
        private readonly ILogService _logService;
        private readonly IInfobankrepository _infobankrepository;
        public InfobancService(ILogService logService, IInfobankrepository infobankrepository)
        {
            _logService = logService ?? throw new ArgumentException(nameof(logService));
            _infobankrepository = infobankrepository ?? throw new ArgumentException(nameof(logService));
        }

        public string ExportarInfobancService(string CNPJ)
        {
             return _infobankrepository.Exportarparainfobanc(CNPJ).Result;   

        }

       
    }
}
