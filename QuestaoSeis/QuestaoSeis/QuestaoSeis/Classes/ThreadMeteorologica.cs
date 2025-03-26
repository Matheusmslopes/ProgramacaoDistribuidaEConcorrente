using QuestaoSeis.Enum;

namespace QuestaoSeis.Classes
{
    public class ThreadMeteorologica
    {
        public string NomeThread { get; set; }
        
        public ThreadMeteorologica(string nomeThread)
        {
            NomeThread = nomeThread;
        }

        public async Task<List<DadosMeteorologicos>> GetData(List<DadosMeteorologicos> lista)
        {
            const int temperaturaMinima = 1;
            const int temperaturaMaxima = 39;
            Random random = new Random();
            int qtdIds = random.Next(100, 200);
            
            for (int i = 0; i < qtdIds; i++)
            {
            Regiao regiaoRandom = (Regiao)random.Next(0, 5);
                lista.Add(new DadosMeteorologicos(
                    i + 1,
                    $"Estação Meteorológica de Número {i + 1}",
                    regiaoRandom,
                    random.Next(temperaturaMinima, temperaturaMaxima)
                ));
            }

            return await Task.FromResult(lista);
        }

    }
}
