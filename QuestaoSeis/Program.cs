using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace QuestaoSeis
{
  class Program
  {
    static void Main(string[] args)
    {
      List<DadosMeteorologicos> lista = new List<DadosMeteorologicos>();
      ThreadMeteorologica thread = new ThreadMeteorologica("Thread 1");
      thread.ColetarData(lista);
      thread.RetornarTemperaturas(lista);
    }
    
    public class DadosMeteorologicos : Entidade
    {
        public string Nome { get; set; }
        public Regiao Regiao { get; set; }
        public double TemperaturaMedia { get; set; }

        public DadosMeteorologicos(int id, string nome, Regiao regiao, double temperaturaMedia)
        {
            Id = id;
            Nome = nome;
            Regiao = regiao;
            TemperaturaMedia = temperaturaMedia;
        }
    }
    
    public class Entidade
    {
        // IDs não devem ser int, mas vamos ignorar nesse cenário para menor complexidade.
        public int Id { get; set; }
    }
    
    public class ThreadMeteorologica
    {
        public string NomeThread { get; set; }
        
        public ThreadMeteorologica(string nomeThread)
        {
            NomeThread = nomeThread;
        }

        public async Task<List<DadosMeteorologicos>> ColetarData(List<DadosMeteorologicos> lista)
        {
            Console.WriteLine("Iniciando a coleta de dados...");
            Random random = new Random();
            const double temperaturaMinima = 1.00;
            const double temperaturaMaxima = 39.00;
            int qtdIds = random.Next(100, 400);
            
            for (int i = 0; i < qtdIds; i++)
            {
            Regiao regiaoRandom = (Regiao)random.Next(0, 5);
                lista.Add(new DadosMeteorologicos(
                    i + 1,
                    $"Estação Meteorológica de Número {i + 1}",
                    regiaoRandom,
                    GetRandomNumber(temperaturaMinima, temperaturaMaxima)
                ));
            }
            Console.WriteLine("Dados coletados.");
            return await Task.FromResult(lista);
        }
        
        public void RetornarTemperaturas(List<DadosMeteorologicos> lista) {
            double somaTotal = 0.00;
            for(int i = 0; i < lista.Count; i++) {
                somaTotal += lista[i].TemperaturaMedia;
            }
            double tempMediaFinal = somaTotal / lista.Count;
            foreach(var item in lista) {
                Console.WriteLine($"{item.Nome} respondeu com {item.TemperaturaMedia}°C");
            }
            Console.WriteLine($"Temperatura Média Final é de {Math.Round(tempMediaFinal, 2)}°C");
        }
    }
        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    
    public enum Regiao
    {
        Norte,
        Nordeste,
        CentroOeste,
        Sudeste,
        Sul
    }
  }
}
