using SQLite.Net.Attributes;

namespace DataModel
{
    public class Pregunta
    {
        [PrimaryKey]
        public int PreguntaId { get; set; }
        public string TextoPregunta { get; set; }
    }
}
