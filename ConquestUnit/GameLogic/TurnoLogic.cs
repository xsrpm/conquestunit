using DataModel;

namespace GameLogic
{
    public static class TurnoLogic
    {
        public static void TerminarTurnoComenzarNuevoTurno(Juego objJuego)
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                if (objJuego.JugadoresConectados[i].Ip==objJuego.IpJugadorTurnoActual)
                {
                    if (i== objJuego.JugadoresConectados.Count-1)
                    {
                        objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[0].Ip;
                        objJuego.TurnoActual = 0;

                    }
                    else
                    {
                        objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[i].Ip;
                        objJuego.TurnoActual = i;
                    }
                    break;
                }
            }
        }
    }
}
