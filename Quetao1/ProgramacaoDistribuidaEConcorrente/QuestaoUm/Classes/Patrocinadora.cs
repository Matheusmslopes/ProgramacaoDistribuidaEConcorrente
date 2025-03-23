namespace QuestaoUm.Classes
{
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
                lock (conta.lockObj)
                {
                    while (conta.Saldo > 0)
                    {
                        Monitor.Wait(conta.lockObj);
                    }
                    conta.Depositar(valorDeposito, this.GetType().Name);
                }
                await Task.Delay(1000); // Pequeno delay para evitar loop muito intenso
            }
        }
    }
}
