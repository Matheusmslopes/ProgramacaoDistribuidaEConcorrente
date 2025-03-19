using System;
using System.Threading;
using System.Threading.Tasks;

class HelloWorld {
  static void Main() {
    Conta conta1 = new Conta(1, "pd", 1000.00);
    Thread thread = new Thread();
    
    thread.AGastadora(conta1);
  }
}

public class Conta {
    public Conta(int numero, string titular, double saldo) {
        Titular = titular;
        Numero = numero;
        Saldo = saldo;
    }
    
    public int Numero {  get; private set; }
    public string Titular {  get; private set; }
    public double Saldo {  get; private set; }
    
    public void Depositar(double valorDeposito) {
        if (valorDeposito > 0) {
        Saldo += valorDeposito;
        Console.WriteLine($"Depósito realizado com sucesso. Saldo atual da conta {Numero}: {Saldo}");
        } else {
            Console.WriteLine("Valor de depósito inválido.");
        }
    }
    
    public void Sacar(double valorSaque) {
        if (valorSaque > 0 && Saldo > valorSaque) {
            Saldo -= valorSaque;
            Console.WriteLine($"Saque realizado com sucesso. Saldo atual da conta {Numero}: {Saldo}");
        } else {
            Console.WriteLine("Valor de saque inválido.");
        }
    }
}

public class Thread {
    public async Task<Conta> AGastadora(Conta conta) {
        while(conta.Saldo >= 10) {
            conta.Sacar(10);
            await Task.Delay(3000);
        }
        return conta;
    }
}
