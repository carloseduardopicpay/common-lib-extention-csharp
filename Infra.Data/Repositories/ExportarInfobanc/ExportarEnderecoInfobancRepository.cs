using Dapper;
using Domain.Interfaces.IRepository;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories.ExportarInfobanc
{
    public class ExportarEnderecoInfobancRepository : IExportarEnderecoInfobancRepository
    {
        string procExportarEndereco = "";

        public Task<string> ExportarEndereco(string cnpj, string connectionString)
        {

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    procExportarEndereco = $" EXEC CORPP_INTEGRA_PESSOAJURIDICA_ENDERECO_INFOBANC  @CGC_CPF = '{cnpj}'";

                    var result = connection.Query<string>(procExportarEndereco).FirstOrDefault();

                    Console.WriteLine($"Exportar Endereco executado com sucesso!");
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar proc: \n{procExportarEndereco}");
                throw new Exception("Erro em ExportarEndereco: " + ex.Message);
            }

        }

    }
}
