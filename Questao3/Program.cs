using System;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static void Main()
    {
      Pedido pedido1 = new Pedido(1);
      
      pedido1.PedidoPronto();
    }
}

public class Pedido
{
  public int numeroPedido { get; private set; }
  
   
    public Pedido(int numero)
    {
        if (numero <= 0)
        {
            throw new ArgumentException("Número de pedido inválido");
        }
        numeroPedido = numero;
       
    }
  
  
  public bool getPipoca(){
      Console.WriteLine("Pipoca Pronta");
      return true;
  }
  
  public bool getRefri(){
      Console.WriteLine($"Refri do pedido {numeroPedido} Pronto");
      return true;
  }
  
  public string PedidoPronto(){
      if (getRefri() && getPipoca()){
          Console.WriteLine($"Pedido {numeroPedido} Pronto");
          return "Pedido pronto"; 
      }
      else{
          return "Pedido não finalizado";
      }
    }
}

    
