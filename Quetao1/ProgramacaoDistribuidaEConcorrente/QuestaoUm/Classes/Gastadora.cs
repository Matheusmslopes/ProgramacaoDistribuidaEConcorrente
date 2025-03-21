public class Gastadora
{
    private readonly Conta conta;
    private int qntdSaque = 0;
    private double totalSacado = 0;

    public Gastadora(Conta conta)
    {
        this.conta = conta;
    }

    public void Run()
    {
        while (true)
        {
            conta.Sacar(10, "Gastadora", ref qntdSaque, ref totalSacado);
            Thread.Sleep(3000);
        }
    }
}