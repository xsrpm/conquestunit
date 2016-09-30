using ConquestUnit.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Juego
    {
        public string JugadorEnTurno;
        public string FasedeJuegoActual;
        public List<int> NFichasParaDesplegar;
    }
}
