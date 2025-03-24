using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program {
  static void Main() {
      // ignorar por enquanto
      List<DadosMeteorologicos> data = new List<DadosMeteorologicos>();
      DadosMeteorologicos dadosMeteorologicos = new DadosMeteorologicos(1 ,"BD1", Regiao.Sudeste, 30.0f);
      data.Add(dadosMeteorologicos);
      var data = dadosMeteorologicos.GetData(data);
  }

public class Entidade {
    // IDs não devem ser int, mas vamos ignorar nesse cenário para menor complexidade.
    public int Id { get; set; }
}

public class DadosMeteorologicos : Entidade {
    public string Nome { get; set; }
    public Regiao Regiao { get; set; }
    public float TemperaturaMedia { get; set; }
    
    public DadosMeteorologicos(int id, string nome, Regiao regiao, float temperaturaMedia) {
        Id = id;
        Nome = nome;
        Regiao = regiao;
        TemperaturaMedia = temperaturaMedia;
    }
    
    public async Task<List<DadosMeteorologicos>> GetData(List<DadosMeteorologicos> lista) {
        Random random = new Random();
        int qtdIds = random.Next(100, 200);
        Array values = Enum.GetValues(typeof(Regiao));
        Regiao regiaoRandom = (Regiao)values.GetValue(random.Next(values.Length));
        foreach(var item in qtdIds) {
            
        }
        Console.WriteLine(regiaoRandom);

        return lista;
    }
}
public class ThreadMeteorologica {
    public string NomeThread {get; set;}
}

public enum Regiao {
    Norte,
    Nordeste,
    CentroOeste,
    Sudeste,
    Sul
}
}
