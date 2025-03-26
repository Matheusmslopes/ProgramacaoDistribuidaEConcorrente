using QuestaoSeis.Enum;

namespace QuestaoSeis.Classes
{
    public class DadosMeteorologicos : Entidade
    {
        public string Nome { get; set; }
        public Regiao Regiao { get; set; }
        public float TemperaturaMedia { get; set; }

        public DadosMeteorologicos(int id, string nome, Regiao regiao, float temperaturaMedia)
        {
            Id = id;
            Nome = nome;
            Regiao = regiao;
            TemperaturaMedia = temperaturaMedia;
        }
    }
}
