using System.Collections.Generic;

namespace DataModel
{
    public class Juego
    {
        //public string FasedeJuegoActual;
        //public List<int> NFichasParaDesplegar;
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
        public string TipoMapa { get; set; }

        public Juego(string tipoMapa)
        {
            JugadoresConectados = new List<Jugador>();
            TipoMapa = tipoMapa;
        }
    }
}
