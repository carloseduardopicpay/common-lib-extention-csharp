using Dapper;
using Domain.Interfaces.IRepository;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories.ExportarInfobanc
{
    public class ExportarPessoasInfobancRepository : IExportarPessoasInfobancRepository
    {
        string procExportarPessoas = "";

        public Task<string> ExportarPessoas(string cnpj, string connectionString)
        {

            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    procExportarPessoas = $" EXEC CORPP_INTEGRA_PESSOAJURIDICA_DADOSBASICOS_INFOBANC  @CGC_CPF = '{cnpj}'";

                    var result = connection.Query<string>(procExportarPessoas).FirstOrDefault();

                    Console.WriteLine($"ExportarPessoas executado com sucesso!");
                    return Task.FromResult(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar proc: \n{procExportarPessoas}");
                throw new Exception("Erro em ExportarPessoas: " + ex.Message);
            }

        }

    }
}
