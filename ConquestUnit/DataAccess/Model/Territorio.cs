using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Territorio
    {
        public int TerritorioId { get; set; }
        public string JugadorDueñoNombre { get; set; }
        public int NUnidadesDesplegadas { get; set; }
    }
}
