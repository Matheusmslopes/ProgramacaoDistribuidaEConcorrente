using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace QuestaoSeis
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadMeteorologica thread1 = new ThreadMeteorologica("Thread 1");
            ThreadMeteorologica thread2 = new ThreadMeteorologica("Thread 2");

            List<DadosMeteorologicos> dadosColetados = ColetarDadosParalelo(thread1, thread2);

            RetornarTemperaturas(dadosColetados);
        }

        static List<DadosMeteorologicos> ColetarDadosParalelo(ThreadMeteorologica thread1, ThreadMeteorologica thread2)
        {
            List<DadosMeteorologicos> lista1 = new List<DadosMeteorologicos>();
            List<DadosMeteorologicos> lista2 = new List<DadosMeteorologicos>();

            Task<List<DadosMeteorologicos>> task1 = thread1.ColetarData(lista1);
            Task<List<DadosMeteorologicos>> task2 = thread2.ColetarData(lista2);

            Task.WhenAll(task1, task2).Wait();

            List<DadosMeteorologicos> dadosColetados = task1.Result;
            dadosColetados.AddRange(task2.Result);

            return dadosColetados;
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
                Console.WriteLine($"Iniciando a coleta de dados da {NomeThread}...");
                Random random = new Random();
                const double temperaturaMinima = 1.00;
                const double temperaturaMaxima = 39.00;
                int qtdIds = random.Next(50, 100);

                for (int i = 0; i < qtdIds; i++)
                {
                    Regiao regiaoRandom = (Regiao)random.Next(0, 5);
                    lista.Add(new DadosMeteorologicos(
                        i + 1,
                        $"{NomeThread}: Estação Meteorológica de Número {i + 1}",
                        regiaoRandom,
                        GetRandomNumber(temperaturaMinima, temperaturaMaxima)
                    ));
                }
                Console.WriteLine($"Dados coletados da {NomeThread}.");
                return await Task.FromResult(lista);
            }
        }

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static void RetornarTemperaturas(List<DadosMeteorologicos> lista)
        {
            double somaTotal = 0.00;
            foreach (var item in lista)
            {
                Console.WriteLine($"{item.Nome} respondeu com {item.TemperaturaMedia}°C");
                somaTotal += item.TemperaturaMedia;
            }
            double tempMediaFinal = somaTotal / lista.Count;
            Console.WriteLine($"Temperatura Média Final é de {Math.Round(tempMediaFinal, 2)}°C");
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
