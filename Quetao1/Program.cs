using System;
using System.Threading;

public class Conta {
    public int Numero {  get; private set; }
    public string Titular {  get; private set; }
    public double Saldo {  get; private set; }
    private readonly object lockObj = new object(); //Estudar como isso funciona (chat)
    
    
    public Conta(int numero, string titular, double saldo) {
        if (numero <= 0  || saldo < 0){
            throw new ArgumentException("Número de conta inválido ou saldo inicial negativo");
        }
        Titular = titular;
        Numero = numero;
        Saldo = saldo;
    }
    
    public bool Sacar(double valorSaque, string nomeThread, ref int qntdSaque, ref double totalSacado) {
        lock (lockObj){ // Estudar como isso funciona (chat)
        //enquanto o saldo estiver zerado, as threads vao entrar em espera e isso vai printar na tela a qntd de 
        //vezes que essa thread sacou e o valor total
            while(Saldo == 0){
                Console.WriteLine($"A thread {nomeThread} entrou em espera. Total de saques: {qntdSaque}, Total retirado: R${totalSacado}");
                Monitor.Wait(lockObj); // Estudar como isso funciona (chat)
            }
        
            if (valorSaque > 0 && Saldo >= valorSaque) {
                Saldo -= valorSaque;
                qntdSaque++;
                totalSacado += valorSaque;
                Console.WriteLine($"A thread {nomeThread} realizou o saque de R${valorSaque} com sucesso. Saldo atual da conta {Numero}: {Saldo}");
                return true;
            } else {
                Console.WriteLine($"A thread {nomeThread} tentou realizar o saque de R${valorSaque}, mas não há saldo suficiente. Saldo atual da conta {Numero}: {Saldo}");
                return false;
            }
        }
    }
    
    public void Depositar(double valorDeposito, string nomeThread) {
        lock (lockObj) {
            if (valorDeposito > 0) {
                Saldo += valorDeposito;
                Console.WriteLine($"A thread {nomeThread} realizou o depósito de R${valorDeposito} com sucesso. Saldo atual da conta {Numero}: {Saldo}");
                Monitor.PulseAll(lockObj);
            } else {
                Console.WriteLine("Valor de depósito inválido.");
            }
        }
    }
    
}

public class Gastadora{
    private readonly Conta conta;
    private int qntdSaque = 0;
    private double totalSacado = 0;
    
    public Gastadora(Conta conta){
        this.conta = conta;
    }
    
    public void Run(){
        while (true) {
            conta.Sacar(10, "Gastadora", ref qntdSaque, ref totalSacado);
            Thread.Sleep(3000);
        }
    }
}


class Banco {
    static void Main(string[] args) {
        Conta conta1 = new Conta(1, "João", 50);
        
        Thread gastadora = new Thread(new Gastadora(conta1).Run);
        
        gastadora.Start();
        
       
    }
}
