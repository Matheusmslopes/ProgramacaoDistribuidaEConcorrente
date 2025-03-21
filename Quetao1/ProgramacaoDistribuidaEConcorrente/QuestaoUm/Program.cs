    Console.WriteLine("Entrou");
    Conta conta1 = new Conta(1, "João", 50);

    Thread gastadora = new Thread(new Gastadora(conta1).Run);
    gastadora.Start();