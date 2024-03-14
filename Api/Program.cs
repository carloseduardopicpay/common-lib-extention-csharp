using Api;
using Infra.CrossCutting.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using System;

public class Program
{
    public static int Main(string[] args)
    {
        DotEnv.CarregaVariaveisAmbienteLocal();
        Logs();

        try
        {
           
            CreateHostBuilder(args).Build().Run();
            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex.Message, "--- SERVIDOR TERMINOU INESPERADMENTE ---");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void Logs()
    {

        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
        .WriteTo.Console(outputTemplate: "{Message}", restrictedToMinimumLevel: LogEventLevel.Information)
        .WriteTo.File(@"..\logs\common-lib.log", outputTemplate: "{Message}", restrictedToMinimumLevel: LogEventLevel.Information)
        .CreateLogger();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
         Host.CreateDefaultBuilder(args)
             .UseSerilog() 
             .ConfigureWebHostDefaults(webBuilder =>
             {
                 webBuilder.UseStartup<Startup>();
             });

}
