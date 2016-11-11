using DataModel;
using System.Linq;
using Util;

namespace GameLogic
{
    public static class DespliegueLogic
    {
        public static void UnidadesDisponiblesDesplegarJugadorTurnoActual(Juego objJuego)
        {
            //Obtener todos los territorios del juegador
            int numeroTerritorios = objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual).Count();
            int bonoTerritorios = 0;
            if (numeroTerritorios >= 7)
                bonoTerritorios++;
            if (numeroTerritorios >= 9)
                bonoTerritorios++;
            if (numeroTerritorios >= 12)
                bonoTerritorios++;
            if (numeroTerritorios >= 15)
                bonoTerritorios++;
            if (numeroTerritorios >= 18)
                bonoTerritorios++;
            if (numeroTerritorios >= 21)
                bonoTerritorios++;
            //Obtner el numero de continentes
            int territoriosRegion1 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual && x.NroRegion == 1).Count();
            int territoriosRegion2 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual && x.NroRegion == 2).Count();
            int territoriosRegion3 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual && x.NroRegion == 3).Count();
            int territoriosRegion4 = objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual && x.NroRegion == 4).Count();
            int bonoRegion1 = territoriosRegion1 == 6 ? Constantes.BonoTerritoriosRegion1 : 0;
            int bonoRegion2 = territoriosRegion2 == 6 ? Constantes.BonoTerritoriosRegion2 : 0;
            int bonoRegion3 = territoriosRegion3 == 6 ? Constantes.BonoTerritoriosRegion3 : 0;
            int bonoRegion4 = territoriosRegion4 == 6 ? Constantes.BonoTerritoriosRegion4 : 0;

            objJuego.UnidadesDisponiblesParaDesplegar = Constantes.BonoUnidadesBaseParaDespliegue + bonoTerritorios + bonoRegion1 + bonoRegion2 + bonoRegion3 + bonoRegion4;
        }
    }
}
