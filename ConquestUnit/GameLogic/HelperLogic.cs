using ConquestUnit;
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

        public static int NroTerritoriosJugador(Juego objJuego, string ipJugador)
        {
            //Obtner el numero de territorios
            return objJuego.Territorios.Where(x => x.IpJugadorPropietario.Equals(ipJugador)).Count();
        }

        public static int NroContinentesJugador(Juego objJuego, string ipJugador)
        {
            //Obtner el numero de continentes
            int territoriosRegion1 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == ipJugador && x.NroRegion == 1).Count();
            int territoriosRegion2 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == ipJugador && x.NroRegion == 2).Count();
            int territoriosRegion3 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == ipJugador && x.NroRegion == 3).Count();
            int territoriosRegion4 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == ipJugador && x.NroRegion == 4).Count();
            int continente1 = territoriosRegion1 == 6 ? 1 : 0;
            int continente2 = territoriosRegion2 == 6 ? 1 : 0;
            int continente3 = territoriosRegion3 == 6 ? 1 : 0;
            int continente4 = territoriosRegion4 == 6 ? 1 : 0;
            return continente1 + continente2 + continente3 + continente4;
        }

        public static Pregunta ObtenerPreguntaAleatoria(ConquestUnitContext context)
        {
            var preguntas = context.conn.Table<Pregunta>();
            return preguntas.ElementAt(Helper.IntUtil.Random(0, preguntas.Count()));
        }

        public static List<Opcion> ObtenerOpciones(ConquestUnitContext context, int idPregunta)
        {
            var opciones = context.conn.Table<Opcion>().Where(x => x.PreguntaId == idPregunta).ToList();
            Helper.Shuffle(opciones);
            return opciones;
        }
    }
}
