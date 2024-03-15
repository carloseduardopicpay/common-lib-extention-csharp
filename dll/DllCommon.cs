using Dapper;
using Domain.Entities;
using System.Data.SqlClient;

namespace dll
{
    public class DllCommon
    {

        string procExportarPessoas = "";
        string procExportarEndereco = "";

        public async Task<string> Exportarparainfobanc(string cnpj)
        {


            using (var connection = new SqlConnection(CreateDbContext()))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var retornoPessoa = await ExportarPessoas(cnpj, CreateDbContext());
                        var retornoEndereco = await ExportarEndereco(cnpj, CreateDbContext());

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

       


        private string CreateDbContext()
        {


            //if (IHostEnvironment.IsDevelopment())
            //{
            string variaveisConexaoLocal = RetornaConexaoLocalEnv();
            if (variaveisConexaoLocal != "") return variaveisConexaoLocal;

            return null;
            //}

            //// Código para outros ambientes (produção, teste, etc.)
            //Console.WriteLine($"Estamos no ambiente: {_env.EnvironmentName}");
            //string VariaveisConexaoVault = RetornaConexaoVault();
            //return VariaveisConexaoVault;

        }


        public string RetornaConexaoVault()
        {
            Console.WriteLine("APP_STAGE ATUAL: " + Environment.GetEnvironmentVariable("APP_STAGE"));

            var databaseConfiguration = new DatabaseConfiguration()
            {
                Db = Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_DB").Trim(),
                Host = Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_HOST").Trim(),
                Pasw = Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_PASSWORD").Trim(),
                Port = int.Parse(Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_PORT")),
                User = Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_USERNAME").Trim()
            };



            if (databaseConfiguration.Host == null || databaseConfiguration.User == null || databaseConfiguration.Pasw == null || databaseConfiguration.Db == null || databaseConfiguration.Port == null)
            {
                Console.WriteLine("Varriaveis do banco de dados :" + databaseConfiguration + " ,não encontradas na memória (vault) , DbContextMemory/RetornaConexaoVault");
                throw new InvalidOperationException("Variáveis de ambiente não encontradas na memória.");
            }


            var ACADConnection = $"Data Source={databaseConfiguration.Host};initial catalog={databaseConfiguration.Db};user id={databaseConfiguration.User};password={databaseConfiguration.Pasw};Integrated Security=False;Min Pool Size=10;Max Pool Size=500;";


            Console.WriteLine(" \r\n  ***** CONECTANDO AO BANCO DE DADOS ***** \n");
            //Console.WriteLine("ConnectionString: " + ACADConnection);


            return ACADConnection;
        }



        private string RetornaConexaoLocalEnv()
        {
            try
            {

                //Recupere os valores das variáveis .ENV    
                string dbServer = "picpay-qa-ppbank-sqlserver.cwh0kanyj0yc.us-east-1.rds.amazonaws.com";
                string dbPort = "1433";
                string dbUser = "usr_acad_service";
                string dbPassword = "q9dTd2At8AHDyHJ";
                string dbName = "BJBS_CORPORATE";

                if (dbServer == null || dbUser == null || dbPassword == null || dbName == null || dbPort == null)
                {
                    Console.WriteLine("Usuario e Host Não encontrados :" + " Usu :" + dbName + " Host :" + dbServer, "Variáveis de ambiente não encontradas na memória (.ENV) DbContextMemory/RetornaConexaoLocalEnv");
                    throw new InvalidOperationException("Variáveis de ambiente não encontradas na memória.");
                }


                var ACADConnection = $"Data Source={dbServer};initial catalog={dbName};user id={dbUser};password={dbPassword};Min Pool Size=10;Max Pool Size=500;";

                return ACADConnection;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao recuperar a connectionstring: {ex}");
                throw;
            }

        }

    }
}
