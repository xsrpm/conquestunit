namespace Util
{
    public static class Constantes
    {
        public static string FILE_NOMBRE_JUGADOR = "nombreJugador.txt";
        public static string FILE_IMAGEN_JUGADOR = "imagenJugador.txt";
        public static string SEPARADOR = ";#;#";
        public static string MessageDialogTitle = "Conquest Unit";

        public static string IconoJugador = "ms-appx:///Assets/Pantallas/PC/RolJugador.png";

        public static string MULTICAST_ADDRESS = "239.10.2.78";
        public static string MULTICAST_SERVICE_PORT = "54330";
        public static string UNICAST_SERVICE_PORT = "22111";
        public static string STREAM_SERVICE_PORT = "22110";
        public static int DELAY = 500;

        public struct MAPA
        {
            public const int CHINA = 1;
            public const int ROMA = 2;
        }

        #region Color Jugadores
        public static string ColorJug1 = "FFB81010";
        public static string ColorJug2 = "FF508A11";
        public static string ColorJug3 = "FF2E507E";
        public static string ColorJug4 = "FFBBC60F";

        public static string ColorPastelJug1 = "FFE7A383";
        public static string ColorPastelJug2 = "FFCCCA76";
        public static string ColorPastelJug3 = "FFB3BFB3";
        public static string ColorPastelJug4 = "FFD9C086";
        #endregion

        public static string ColorCorrecto = "#FF00FF00";
        public static string ColorIncorrecto = "#FFFF0000";

        #region Asset Imagenes Unidades Jugadores
        public static string UnidadJug1CHINA = "ms-appx:///Assets/Unidades/China/Unidad Rojo.png";
        public static string UnidadJug2CHINA = "ms-appx:///Assets/Unidades/China/Unidad Verde.png";
        public static string UnidadJug3CHINA = "ms-appx:///Assets/Unidades/China/Unidad Azul.png";
        public static string UnidadJug4CHINA = "ms-appx:///Assets/Unidades/China/Unidad Amarillo.png";
        public static string UnidadAgrupadoraJug1CHINA = "ms-appx:///Assets/Unidades/China/IratoRojo.png";
        public static string UnidadAgrupadoraJug2CHINA = "ms-appx:///Assets/Unidades/China/IratoVerde.png";
        public static string UnidadAgrupadoraJug3CHINA = "ms-appx:///Assets/Unidades/China/IratoAzul.png";
        public static string UnidadAgrupadoraJug4CHINA = "ms-appx:///Assets/Unidades/China/IratoAmarillo.png";

        public static string UnidadJug1ROMA = "ms-appx:///Assets/Unidades/Roma/Unidad Rojo.png";
        public static string UnidadJug2ROMA = "ms-appx:///Assets/Unidades/Roma/Unidad Verde.png";
        public static string UnidadJug3ROMA = "ms-appx:///Assets/Unidades/Roma/Unidad Azul.png";
        public static string UnidadJug4ROMA = "ms-appx:///Assets/Unidades/Roma/Unidad Amarillo.png";
        public static string UnidadAgrupadoraJug1ROMA = "ms-appx:///Assets/Unidades/Roma/CatapultaRojo.png";
        public static string UnidadAgrupadoraJug2ROMA = "ms-appx:///Assets/Unidades/Roma/CatapultaVerde.png";
        public static string UnidadAgrupadoraJug3ROMA = "ms-appx:///Assets/Unidades/Roma/CatapultaAzul.png";
        public static string UnidadAgrupadoraJug4ROMA = "ms-appx:///Assets/Unidades/Roma/CatapultaAmarillo.png";
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
        public static string MesaConumicaVICTORIAFinDelJuego = "MesaConumicaVICTORIAFinDelJuego";
        public static string MesaConumicaDERROTAFinDelJuego = "MesaConumicaDERROTAFinDelJuego";
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

        public static string COLORMANDOACTIVO = "FF01FE0F";
        public static string COLORMANDOINACTIVO = "FFFF0437";

        public struct Region
        {
            public const int REGION1 = 1;
            public const int REGION2 = 2;
            public const int REGION3 = 3;
            public const int REGION4 = 4;
        } 

        public struct MensajesResultadoBatalla
        {
            public const string GANASTE = "GANASTE";
            public const string DERROTA = "DERROTA";

            public const string CONTINUAR_ATAQUE = "¿CONTINUAR EL ATAQUE?";
            public const string DESPLIEGA_UNIDADES = "DESPLIEGA TUS UNIDADES";
            public const string RESPONDER_SIGUIENTE_PREGUNTA = "RESPONDER SIGUIENTE PREGUNTA";
        }

        public const string MOVER_TROPAS_MOVER = "MOVIENDO UNIDADES";
        public const string MOVER_TROPAS_FORTIFICACION = "FORTIFICACION";

        public struct MensajesConfirmarContinuar
        {
            public const string DespliegueContinuar = "Se acabaron las unidades para desplegar";
            public const string DespliegueConfirmar = "¿Desea concluir la fase de Despliegue?";

            public const string AtaqueConfirmar = "¿Desea concluir la fase de Ataque?";

            public const string FortificarConfirmar = "¿Desea concluir la fase de Fortificación?";
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
            public const int DESPLEGAR_FIN_DESPLIGUE_CONFIRMAR = 1;
            public const int DESPLEGAR_FIN_DESPLIGUE_CONTINUAR = 2;

            public const int ELEGIRORIGENATK = 3;
            public const int ELEGIRDESTINOATK = 4;
            public const int CONFIRMARATAQUE = 5;
            public const int BATALLA_PRIMERA_PREGUNTA = 6;
            public const int CONFIRMAR_INICIO_SEGUNDA_PREGUNTA = 7;
            public const int BATALLA_SEGUNDA_PREGUNTA = 8;
            public const int DECIDIR_CONTINUAR_ATAQUE = 9;
            public const int CONFIRMAR_INICIO_MOVERTROPAS = 10;
            public const int MOVERTROPAS = 11;
            public const int TERMINAR_ATAQUE = 12;
            public const int ATAQUE_FIN_ATAQUE_CONTINUAR = 13;

            public const int ELEGIRORIGENFOR = 14;
            public const int ELEGIRDESTINOFOR = 15;
            public const int CONFIRMAR_FORTIFICACION = 16;
            public const int FORTIFICAR = 17;
            public const int FORTIFICAR_FIN_CONFIRMAR = 18;
            public const int FORTIFICAR_FIN_CONTINUAR = 19;
        }

        #region NroUnidades
        public static int BonoUnidadesBaseParaDespliegue = 2;
        public static int BonoTerritoriosRegion1 = 4;
        public static int BonoTerritoriosRegion2 = 3;
        public static int BonoTerritoriosRegion3 = 2;
        public static int BonoTerritoriosRegion4 = 3;

        public static int UnidadesMinimasParaEvolucionar = 5;
        #endregion
    }
}
