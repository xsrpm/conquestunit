using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util
{
    public static class Constantes
    {
        public static string FILE_NOMBRE_JUGADOR = "nombreJugador.txt";
        public static string FILE_IMAGEN_JUGADOR = "imagenJugador.txt";
        public static string SEPARADOR = ";#;#";
        public static string MessageDialogTitle = "Conquest Unit";

        public static string MULTICAST_ADDRESS = "239.10.2.78";
        public static string MULTICAST_SERVICE_PORT = "54330";
        public static string UNICAST_SERVICE_PORT = "22111";
        public static string STREAM_SERVICE_PORT = "22110";
        public static int DELAY = 500;

        public static string MAPA_CHINA = "CHINA";

        #region Color Jugadores
        public static string ColorJug1 = "#E7A383";
        public static string ColorJug2 = "#CCCA76";
        public static string ColorJug3 = "#B3BFB3";
        public static string ColorJug4 = "#D9C086";
        #endregion

        #region Asset Imagenes Unidades Jugadores
        public static string UnidadJug1 = "ms-appx:///Assets/Unidades/China/Unidad Rojo.png";
        public static string UnidadJug2 = "ms-appx:///Assets/Unidades/China/Unidad Verde.png";
        public static string UnidadJug3 = "ms-appx:///Assets/Unidades/China/Unidad Azul.png";
        public static string UnidadJug4 = "ms-appx:///Assets/Unidades/China/Unidad Amarillo.png";
        #endregion

        #region Mensajes para unirse a la mesa
        public static string UnirseEnviameConfirmacion = "UnirseEnviameConfirmacion";
        public static string ConfirmacionUnirseMesa = "ConfirmacionUnirseMesa";
        public static string JugadorSaleMesa = "JugadorSaleMesa";
        public static string MesaIndicaSeCierra = "MesaIndicaSeCierra";
        public static string MesaIndicaJuegoInicia = "MesaIndicaJuegoInicia";
        #endregion

        #region Mensajes de Juego
        public static string MesaConumicaHABILITARControles = "MesaConumicaHABILITARControles";
        public static string MesaConumicaDESHABILITARControles = "MesaConumicaDESHABILITARControles";
        public static string JugadorPresionaBoton = "JugadorPresionaBoton";
        public struct Controles
        {
            public const int ARRIBA = 0;
            public const int DERECHA = 1;
            public const int ABAJO = 2;
            public const int IZQUIERDA = 3;

            public const int CUADRADO = 4;
            public const int CIRCULO = 5;
            public const int TRIANGULO = 6;
            public const int EQUIS = 7;
        }
        #endregion

        public static string SIN_IMAGEN = "Ninguno";

        public enum FasesDeJuego { Despliegue, Ataque, Fortificación };
        public enum ResultadoBatalla { Victoria, Derrota };

        public struct MoverTerritorio
        {
            public const int ARRIBA = 0;
            public const int DERECHA = 1;
            public const int ABAJO = 2;
            public const int IZQUIERDA = 3;
        }

        public struct FaseJuego
        {
            public const int DESPLIEGUE = 0;
            public const int ATAQUE = 1;
            public const int FORTIFICACION = 2;
        }

        public struct AccionJuego
        {
            public const int DESPLEGAR = 0;
            public const int ELEGIRORIGENATK = 1;
            public const int ELEGIRDESTINOATK = 2;
            public const int ATACAR = 3;
            public const int BATALLA = 4;
            public const int MOVERTROPAS = 5;
            public const int ELEGIRORIGENFOR = 6;
            public const int ELEGIRDESTINOFOR = 7;
            public const int FORTIFICAR = 8;
        }

        #region NroUnidades
        public static int BonoUnidadesBaseParaDespliegue = 2;
        public static int BonoTerritoriosRegion1 = 4;
        public static int BonoTerritoriosRegion2 = 3;
        public static int BonoTerritoriosRegion3 = 2;
        public static int BonoTerritoriosRegion4 = 3;
        #endregion
    }
}
