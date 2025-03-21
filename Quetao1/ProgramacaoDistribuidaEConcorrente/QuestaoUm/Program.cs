using QuestaoUm.Classes;

// Instanciando a conta
Conta conta1 = new(1, "João", 50);

// Instanciando as threads
Thread gastadora = new Thread(() => new Gastadora(conta1).Run().Wait());
Thread esperta = new Thread(() => new Esperta(conta1).Run().Wait());

// Iniciando as threads
esperta.Start();
gastadora.Start();