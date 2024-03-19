using Dapper;
using System.Data.SqlClient;

namespace Common
{
    public class ExportarInfobanc
    {
        string procExportarPessoas = "";
        string procExportarEndereco = "";
        public async Task<string> Exportarparainfobanc(string cnpj, string conexao)
        {

            using (var connection = new SqlConnection(conexao))
            {
                if(connection.State == System.Data.ConnectionState.Closed) connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var retornoPessoa = await ExportarPessoas(cnpj, conexao);
                        var retornoEndereco = await ExportarEndereco(cnpj, conexao);

                        await transaction.CommitAsync();

                        var RetornoFormatado = $"Retorno ExportarPessoas : {retornoPessoa} \nRetorno Exportar Endereço: {retornoEndereco}";
                        Console.ForegroundColor = ConsoleColor.Blue;

                        Console.WriteLine(RetornoFormatado);
                        return await Task.FromResult(RetornoFormatado);

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        //_logService.LogDefault(ex.Message, ex.StackTrace, "Infra/Data/Repositories/ExportarInfobanc/Exportarparainfobanc", "ERRO");

                        await transaction.RollbackAsync();
                        throw new Exception("Erro em Exportarparainfobanc \n" + ex.Message);
                    }
                }
            }
        }


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
