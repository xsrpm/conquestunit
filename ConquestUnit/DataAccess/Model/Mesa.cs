using ConquestUnit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Mesa
    {
        public string MesaID { get; set; }
        public string Ip { get; set; }
        public List<Jugador> JugadoresConectados{ get; set; }

        public Mesa()
        {
            JugadoresConectados = new List<Jugador>();
        }
    }
}
