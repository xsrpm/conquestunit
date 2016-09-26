using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace ConquestUnit.Model
{
    public class Opcion
    {
        [PrimaryKey]
        public int OpcionId { get; set; }
        public string TextoOpcion { get; set; }
        public bool EsRespuesta { get; set; }

        [ForeignKey(typeof(Pregunta))]
        public int PreguntaId { get; set; }
    }
}
