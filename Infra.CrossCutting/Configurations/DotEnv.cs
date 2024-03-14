using Serilog;
using System;
using System.IO;

namespace Infra.CrossCutting.Configurations
{
    public class DotEnv
    {
        
        public static void Load(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;

                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }

        public static void CarregaVariaveisAmbienteLocal()
        {
            var appRoot = Directory.GetCurrentDirectory();
            var dotEnv = Path.Combine(appRoot, "../.env");
            DotNetEnv.Env.Load("../.env");
       
            
            //Carrega .Env Local
            DotNetEnv.Env.GetString("PICPAYBANK_ACAD_SQLSERVER_USERNAME").Trim();
            DotNetEnv.Env.GetString("PICPAYBANK_ACAD_SQLSERVER_PASSWORD").Trim();
            DotNetEnv.Env.GetString("PICPAYBANK_ACAD_SQLSERVER_DB").Trim();
            DotNetEnv.Env.GetString("PICPAYBANK_ACAD_SQLSERVER_HOST").Trim();
            DotNetEnv.Env.GetInt("PICPAYBANK_ACAD_SQLSERVER_PORT");

            Log.Information("O .env :" + dotEnv);

            Load(dotEnv);
        }
        
      
    }
}
