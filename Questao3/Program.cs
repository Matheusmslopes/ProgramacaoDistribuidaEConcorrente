using System;
using System.Threading.Tasks;

class Program
{
    private static int pedidoNumero = 0; 

    static void Main(string[] args)
    {
        Console.WriteLine("Pedidos chegando");

        for (int i = 0; i < 5; i++)
        {
            FazerPedido().GetAwaiter().GetResult();
        }

        Console.WriteLine("\nTodos os pedidos foram finalizados!");
    }

    static async Task FazerPedido()
    {
        int numeroPedido = ++pedidoNumero; 
        Console.WriteLine($"\nPedido {numeroPedido} recebido! Preparando o seu combo");
        
        Task<string> pipocaTask = GetPipoca(numeroPedido);
        Task<string> refrigeranteTask = GetRefrigerante(numeroPedido);
        
        await Task.WhenAll(pipocaTask, refrigeranteTask);

       

        
    }

   
    static async Task<string> GetPipoca(int pedido)
    {
       
    }

    static async Task<string> GetRefrigerante(int pedido)
    {
        
    }

    static string LanchePronto(int pedido, string pipoca, string refrigerante)
    {
        return $"{pipoca} e {refrigerante}. Aproveite!\n Pedido {pedido} finalizado.";
    }
}
