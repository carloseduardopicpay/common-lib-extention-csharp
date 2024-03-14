using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Infra.Data.Contexts
{
    public class DbContextMemory
    {
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _env;

        public DbContextMemory(IConfiguration configuration, IHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        public string CreateDbContext()
        {

            if (_env.IsDevelopment())
            {
                Console.WriteLine("Estamos no ambiente de desenvolvimento.");
                string variaveisConexaoLocal = RetornaConexaoLocalEnv();
                if (variaveisConexaoLocal != "") return variaveisConexaoLocal;
            }
            
                // Código para outros ambientes (produção, teste, etc.)
                Console.WriteLine($"Estamos no ambiente: {_env.EnvironmentName}");
                string VariaveisConexaoVault = RetornaConexaoVault();
                return VariaveisConexaoVault;
                      
        }


        private string RetornaConexaoVault()
        {
            Console.WriteLine("APP_STAGE ATUAL: " + Environment.GetEnvironmentVariable("APP_STAGE"));

            var databaseConfiguration = new DatabaseConfiguration()
            {
                Db   = Environment.GetEnvironmentVariable("PICPAYBANK_ACAD_SQLSERVER_DB").Trim(),
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
                string dbServer = _configuration["PICPAYBANK_ACAD_SQLSERVER_HOST"].Trim();
                string dbPort = _configuration["PICPAYBANK_ACAD_SQLSERVER_PORT"].Trim();
                string dbUser = _configuration["PICPAYBANK_ACAD_SQLSERVER_USERNAME"].Trim();
                string dbPassword = _configuration["PICPAYBANK_ACAD_SQLSERVER_PASSWORD"].Trim();
                string dbName = _configuration["PICPAYBANK_ACAD_SQLSERVER_DB"].Trim();

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


