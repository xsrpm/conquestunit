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
        public static string ColorJug1 = "FFB81010";
        public static string ColorJug2 = "FF508B10";
        public static string ColorJug3 = "FF2E507E";
        public static string ColorJug4 = "FFBBC60E";

        public static string ColorJug1Inactivo = "AFB81010";
        public static string ColorJug2Inactivo = "AF508B10";
        public static string ColorJug3Inactivo = "AF2E507E";
        public static string ColorJug4Inactivo = "AFBBC60E";
        #endregion

        public static string ColorCorrecto = "#FF00FF00";
        public static string ColorIncorrecto = "#FFFF0000";

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
            public const int ARRIBADERECHA = 1;
            public const int DERECHA = 2;
            public const int ABAJODERECHA = 3;
            public const int ABAJO = 4;
            public const int ABAJOIZQUIERDA = 5;
            public const int IZQUIERDA = 6;
            public const int ARRIBAIZQUIERDA = 7;

            public const int CUADRADO = 8;
            public const int CIRCULO = 9;
            public const int TRIANGULO = 10;
            public const int EQUIS = 11;
        }

        public struct Region
        {
            public const int REGION1 = 1;
            public const int REGION2 = 2;
            public const int REGION3 = 3;
            public const int REGION4 = 4;
        } 

        public struct MensajesResultadoBatalla
        {
            public const string GanaAtacante = "Has ganado la batalla.";
            public const string GanaAtacanteTerritorio = "Has ganado la batalla. Despliega tus unidades.";
            public const string AtacanteResponderaSegundaPregunta = "Perdiste, pero aun puedes ganar.";

            public const string PierdeAtacante = "Perdiste el ataque.";
            public const string GanaDefensor = "Defendiste tu territorio satisfactoriamente.";
            public const string PierdeDefensor = "No lograste defender tu territorio.";
        }

        public struct MensajesConfirmarContinuar
        {
            public const string DespliegueContinuar = "Ya no quedan Unidades. Se pasará al Ataque.";
            public const string DespliegueConfirmar = "¿Desea concluir la fase de Despliegue?";

            //public const string DespliegueContinuar = "Ya no quedan Unidades. Se pasará al Ataque.";
            public const string AtaqueConfirmar = "¿Desea concluir la fase de Ataque?";
        }
        #endregion

        public static string SIN_IMAGEN = "Ninguno";

        public struct FaseJuego
        {
            public const int DESPLIEGUE = 0;
            public const int ATAQUE = 1;
            public const int FORTIFICACION = 2;
        }

        public struct AccionJuego
        {
            public const int DESPLEGAR = 0;
            public const int DESPLEGAR_FIN_DESPLIGUE_CONFIRMAR = 14;
            public const int DESPLEGAR_FIN_DESPLIGUE_CONTINUAR = 15;
            public const int ELEGIRORIGENATK = 1;
            public const int ELEGIRDESTINOATK = 2;
            public const int CONFIRMARATAQUE = 3;
            public const int BATALLA_PRIMERA_PREGUNTA = 4;
            public const int CONFIRMAR_INICIO_SEGUNDA_PREGUNTA = 5;
            public const int BATALLA_SEGUNDA_PREGUNTA = 6;
            public const int DECIDIR_CONTINUAR_ATAQUE = 7;
            public const int CONFIRMAR_INICIO_MOVERTROPAS = 8;
            public const int MOVERTROPAS = 9;
            public const int TERMINAR_ATAQUE = 10;
            public const int ATAQUE_FIN_ATAQUE_CONTINUAR = 15;
            public const int ELEGIRORIGENFOR = 11;
            public const int ELEGIRDESTINOFOR = 12;
            public const int FORTIFICAR = 13;
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
