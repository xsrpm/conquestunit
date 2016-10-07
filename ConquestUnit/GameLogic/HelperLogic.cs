using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace GameLogic
{
    public static class HelperLogic
    {
        //Setea las variables del objeto Juego del siguiente jugador
        public static void TerminarTurno(Juego objJuego)
        {
            if (objJuego.TurnoActual + 1 == objJuego.JugadoresConectados.Count)
            {
                objJuego.TurnoActual = 0;
                objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[0].Ip;
            }
            else
            {
                objJuego.TurnoActual++;
            }
            objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[objJuego.TurnoActual].Ip;
            objJuego.IpJugadorDefiende = "";
        }
    }
}
