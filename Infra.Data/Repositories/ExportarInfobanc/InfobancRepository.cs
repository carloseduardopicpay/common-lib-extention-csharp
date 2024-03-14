using Domain.Entities;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;
using Infra.Data.Contexts;
using Microsoft.Data.SqlClient;

namespace Infra.Data.Repositories.ExportarInfobanc
{

    public class InfobancRepository : IInfobankrepository
    {
        private readonly DbContextMemory _dbContextMemory;
        private readonly ILogService _logService;
        private readonly IExportarPessoasInfobancRepository _pessoasInfobanc;
        private readonly IExportarEnderecoInfobancRepository _exportarEnderecoInfobanc;
        string _connectionString;
        public InfobancRepository(
            DbContextMemory dbContextMemory, 
            ILogService logService,  
            IExportarPessoasInfobancRepository pessoasInfobanc,
            IExportarEnderecoInfobancRepository exportarEnderecoInfobanc)
        {
            _logService = logService;
            _dbContextMemory = dbContextMemory;
            _connectionString = _dbContextMemory.CreateDbContext();
            _pessoasInfobanc = pessoasInfobanc;
            _exportarEnderecoInfobanc = exportarEnderecoInfobanc;
        }

        public async Task<string> Exportarparainfobanc(string cnpj)
        {

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                       var retornoPessoa   = await _pessoasInfobanc.ExportarPessoas(cnpj, _connectionString);
                       var retornoEndereco = await _exportarEnderecoInfobanc.ExportarEndereco(cnpj, _connectionString);

                       await transaction.CommitAsync();

                        var RetornoFormatado =  $"Retorno ExportarPessoas : {retornoPessoa} \nRetorno Exportar Endereço: {retornoEndereco}";
                        Console.ForegroundColor = ConsoleColor.Blue;

                        Console.WriteLine(RetornoFormatado); 
                       return await Task.FromResult(RetornoFormatado);

                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        _logService.LogDefault(ex.Message, ex.StackTrace, "Infra/Data/Repositories/ExportarInfobanc/Exportarparainfobanc", "ERRO");

                        await transaction.RollbackAsync();
                        throw new Exception("Erro em Exportarparainfobanc \n"+ ex.Message);
                    }
                }
            }
        }
        

        private ClassificacaoPessoa ObterPapelParaInfobanc(Pessoa pessoa)
        {
            ClassificacaoPessoa papel;

            if (pessoa.Classificacoes?.OfType<Cliente>().Count() > 0) papel = pessoa.Classificacoes.OfType<Cliente>().FirstOrDefault();
            else if (pessoa.Classificacoes.OfType<Prospecto>().Count() > 0) papel = pessoa.Classificacoes.OfType<Prospecto>().FirstOrDefault();

            papel = pessoa.Classificacoes.LastOrDefault();

            papel.Pessoa = pessoa;

            return papel;
        }
       

    }
}
