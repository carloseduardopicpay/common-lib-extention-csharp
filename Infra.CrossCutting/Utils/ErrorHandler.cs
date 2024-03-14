using System;
using System.Diagnostics;

namespace Infra.CrossCutting.Utils
{
    public class ErrorHandler
    {
        public static void HandleError(Exception ex, string customMessage = null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nOcorreu um erro:");

            if (!string.IsNullOrEmpty(customMessage))
                Console.WriteLine($"Mensagem personalizada: {customMessage}");

            Console.WriteLine($"Tipo de exceção: {ex.GetType().Name}");
            Console.WriteLine($"Mensagem de exceção: {ex.Message}");

            //TODO: Adicionar aqui qualquer lógica adicional desejada para manipular ou registrar o erro.

            Console.ResetColor();
        }

        public static T TryExecute<T>(Func<T> action, string customErrorMessage = null)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                HandleError(ex, customErrorMessage);
                return default; // Pode ser ajustado dependendo do contexto.
            }
        }

        public static void TryExecute(Action action, string customErrorMessage = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                HandleError(ex, customErrorMessage);
            }
        }
    }
}

/*
    HandleError: Este método é usado para exibir informações detalhadas sobre a exceção,
    como tipo de exceção, mensagem e qualquer mensagem personalizada que você queira fornecer. 
    Você pode personalizar esse método de acordo com os requisitos do seu aplicativo.


    TryExecute<T>: Este método envolve a execução de uma função e trata automaticamente 
    qualquer exceção que possa ocorrer durante a execução. Ele retorna o resultado da 
    função ou um valor padrão, dependendo do contexto. Isso ajuda a evitar a propagação 
    de exceções não tratadas.

    TryExecute: Semelhante ao TryExecute<T>, mas para métodos que não retornam um valor.

    Ao usar esta classe, você pode encapsular chamadas a métodos potencialmente falhos em
    blocos TryExecute para garantir que os erros sejam tratados de maneira consistente. 
    Isso facilita a manutenção e aprimoramento da lógica de tratamento de erros em toda a aplicação.
 */