using System.Collections.Generic;

namespace DataModel
{
    public class Juego
    {
        public int FaseActual { get; set; }
        public int AccionActual { get; set; }
        public int TurnoActual { get; set; }
        public string IpJugadorTurnoActual { get; set; }

        #region Despliegue
        public int UnidadesDisponiblesParaDesplegar { get; set; }
        #endregion
        #region Ataque
        public string IpJugadorDefiende { get; set; }
        public Territorio TerritorioAtaqueOrigen { get; set; }
        public Territorio TerritorioAtaqueDestino { get; set; }
        public Pregunta Pregunta { get; set; }
        public List<Opcion> Opciones { get; set; }
        #endregion
        #region Fortificacion
        public Territorio TerritorioFortificacionOrigen { get; set; }
        public Territorio TerritorioFortificacionDestino { get; set; }
        #endregion

        public string JuegoID { get; set; }
        public string Ip { get; set; }
        public List<Jugador> JugadoresConectados { get; set; }
        public List<Territorio> Territorios { get; set; }
        public int TipoMapa { get; set; }

        public Juego()
        {
            JugadoresConectados = new List<Jugador>();
        }

        public Juego(int tipoMapa)
        {
            JugadoresConectados = new List<Jugador>();
            TipoMapa = tipoMapa;
        }

        public Jugador JugadorTurnoActual()
        {
            return JugadoresConectados[TurnoActual];
        }
    }
}
