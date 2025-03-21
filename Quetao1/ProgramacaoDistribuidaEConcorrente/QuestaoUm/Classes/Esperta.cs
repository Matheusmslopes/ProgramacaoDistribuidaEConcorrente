namespace QuestaoUm.Classes
{
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
}
