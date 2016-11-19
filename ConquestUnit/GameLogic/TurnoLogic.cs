using DataModel;

namespace GameLogic
{
    public static class TurnoLogic
    {
        public static void TerminarTurnoComenzarNuevoTurno(Juego objJuego)
        {
            var turnoIndice = objJuego.TurnoActual;
            turnoIndice++;
            if (turnoIndice == objJuego.JugadoresConectados.Count)
            {
                turnoIndice = 0;
            }
            for (int i = turnoIndice; i < objJuego.JugadoresConectados.Count; i++)
            {
                if (objJuego.JugadoresConectados[i].Activo)
                {
                    turnoIndice = i;
                    break;
                }
                if (i == objJuego.JugadoresConectados.Count - 1)
                {
                    i = 0;
                }
            }
            objJuego.TurnoActual = turnoIndice;
            objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[turnoIndice].Ip;
        }
    }
}
