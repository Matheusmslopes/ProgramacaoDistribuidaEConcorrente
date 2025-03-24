using System;
using System.Threading.Tasks;
using System.Threading;

class Program
{
    static void Main()
    {
        Conta conta1 = new(1, "João", 500);

// Instanciando as threads
Thread gastadora = new Thread(() => new Gastadora(conta1).Run().Wait());
Thread esperta = new Thread(() => new Esperta(conta1).Run().Wait());
Thread economica = new Thread(() => new Economica(conta1).Run().Wait());
Thread patrocinadora = new Thread(() => new Patrocinadora(conta1).Run().Wait());

// Iniciando as threads
esperta.Start();
gastadora.Start();
economica.Start();
patrocinadora.Start();
    }
}

public class Gastadora
{
    private readonly Conta conta;
    private int qntdSaque = 0;
    private double totalSacado = 0;

    public Gastadora(Conta conta)
    {
        this.conta = conta;
    }

    public async Task Run()
    {
        while (true)
        {
            conta.Sacar(10, this.GetType().Name, ref qntdSaque, ref totalSacado);
            await Task.Delay(3000);
        }
    }
}

public class Patrocinadora
{
    private readonly Conta conta;
    private readonly double valorDeposito;

    public Patrocinadora(Conta conta, double valorDeposito = 100)
    {
        this.conta = conta;
        this.valorDeposito = valorDeposito;
    }

    public async Task Run()
    {
        while (true)
        {
            lock (conta.lockObj2)
            {
                while (conta.Saldo == 0)
                {
                    
        Console.WriteLine("Entrou");
                conta.Depositar(valorDeposito, this.GetType().Name);
                }
            }
            await Task.Delay(1000); // Pequeno delay para evitar loop muito intenso
        }
    }
}

public class Conta
{
    public int Numero { get; private set; }
    public string Titular { get; private set; }
    public double Saldo { get; private set; }
    public readonly object lockObj1 = new object();
    public readonly object lockObj2 = new object();

    public Conta(int numero, string titular, double saldo)
    {
        if (numero <= 0 || saldo < 0)
        {
            throw new ArgumentException("Número de conta inválido ou saldo inicial negativo");
        }
        Titular = titular;
        Numero = numero;
        Saldo = saldo;
    }

    public bool Sacar(double valorSaque, string nomeThread, ref int qntdSaque, ref double totalSacado)
    {
        lock (lockObj1)
        {
            // Enquanto o saldo estiver zerado, as threads vão entrar em espera e isso vai printar na tela a qntd de
            // vezes que essa thread sacou e o valor total
            if (Saldo == 0)
            {
                Console.WriteLine($"A thread {nomeThread} entrou em espera. Total de saques: {qntdSaque}, Total retirado: R${totalSacado}");
                Monitor.Wait(lockObj1); // Estudar como isso funciona
                
            }

            if (valorSaque > 0 && Saldo >= valorSaque)
            {
                Saldo -= valorSaque;
                qntdSaque++;
                totalSacado += valorSaque;
                Console.WriteLine($"A thread {nomeThread} realizou o saque de R${valorSaque} com sucesso. Saldo atual da conta {Numero}: {Saldo}");
                return true;
            }
            else
            {
                Console.WriteLine($"A thread {nomeThread} tentou realizar o saque de R${valorSaque}, mas não há saldo suficiente. Saldo atual da conta {Numero}: {Saldo}");
                return false;
            }
        }
    }

    public void Depositar(double valorDeposito, string nomeThread)
    {
        lock (lockObj1)
        {
            if (valorDeposito > 0)
            {
                Saldo += valorDeposito;
                Console.WriteLine($"A thread {nomeThread} realizou o depósito de R${valorDeposito} com sucesso. Saldo atual da conta {Numero}: {Saldo}");
                Monitor.PulseAll(lockObj1);
            }
            else
            {
                Console.WriteLine("Valor de depósito inválido.");
            }
        }
    }
}

public class Esperta
    {
        private int qntdSaque = 0;
        private double totalSacado = 0;
        private readonly Conta conta;

        public Esperta(Conta conta)
        {
            this.conta = conta;
        }

        public async Task Run()
        {
            while (true)
            {
                conta.Sacar(50, this.GetType().Name, ref qntdSaque, ref totalSacado);
                await Task.Delay(6000);
            }
        }
    }
    public class Economica
    {
        private readonly Conta conta;
        private int qntdSaque = 0;
        private double totalSacado = 0;

        public Economica(Conta conta)
        {
            this.conta = conta;
        }

        public async Task Run()
        {
            while (true)
            {
                conta.Sacar(5, this.GetType().Name, ref qntdSaque, ref totalSacado);
                await Task.Delay(12000);
            }
        }
    }
    
