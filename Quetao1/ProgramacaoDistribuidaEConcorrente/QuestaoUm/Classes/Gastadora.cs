namespace QuestaoUm.Classes
{
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
}