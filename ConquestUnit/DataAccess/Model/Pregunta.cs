using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace ConquestUnit.Model
{
    public class Pregunta
    {
        [PrimaryKey]
        public int PreguntaId { get; set; }
        public string TextoPregunta { get; set; }
    }
}
