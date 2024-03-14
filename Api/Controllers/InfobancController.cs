using Domain.Entities;
using Domain.Interfaces.IServices;
using Infra.CrossCutting.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Netco.Logging;
using System;
using System.Collections;
using System.Threading.Tasks;
using Volo.Abp;
using static Infra.CrossCutting.Enums.Enums;

namespace Api.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class InfobancController : ControllerBase
    {
        private readonly IInfobancService _infobanc;
        private readonly ILogService _log;

        public InfobancController(IInfobancService infobancService, ILogService log)
        {
            _infobanc = infobancService ?? throw new ArgumentException(nameof(infobancService));
            _log = log;
        }



        [HttpPut("PessoaJuridicaDadosBasicosInfobanc")]
        public string ExportarInfobanc(string CNPJ)
        {
            try
            {
                bool CnpjValido = ValidaCNPJ.ValidarCNPJ(CNPJ);
                if (!CnpjValido) throw new BusinessException($"O Cnpj {CNPJ} é {(CnpjValido ? "VÁLIDO" : "INVÁLIDO")}");


                return  _infobanc.ExportarInfobancService(CNPJ);
               
            }
            catch (Exception ex)
            {
                _log.LogDefault(ex.Message, ex.StackTrace, Url.RouteUrl(RouteData.Values), log_levels.ERRO.ToString());
                Domain.Entities.ErrorHandler.HandleError(ex);
                throw new Exception($"Erro em {Url.RouteUrl(RouteData.Values)} \n" + ex.Message);
            }
        }


        [HttpGet("EnvironmentVariables")]
        public IEnumerable GetVariables()
        {
            try
            {
                var VarAmbient = Environment.GetEnvironmentVariables();
                Console.WriteLine("\nRequisição em: " + Url.RouteUrl(RouteData.Values));

                return VarAmbient;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro em  Variables : " + ex.Message);
            }
        }


    }
}

