using System.Threading.Tasks;
using QuestaoSeis.Classes;
using QuestaoSeis.Enum;

class Program
{
    static async Task Main()
    {
        List<DadosMeteorologicos> lista = new List<DadosMeteorologicos>();
        DadosMeteorologicos dados = new DadosMeteorologicos(1, "Estação 1", Regiao.Norte, 22.5f);
        ThreadMeteorologica thread = new ThreadMeteorologica("Thread 1");

    }
}