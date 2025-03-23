using QuestaoUm.Classes;

// Instanciando a conta
Conta conta1 = new(1, "João", 50);

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
