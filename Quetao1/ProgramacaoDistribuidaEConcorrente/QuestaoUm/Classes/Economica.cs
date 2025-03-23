namespace QuestaoUm.Classes
{
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
}
