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

        #region Mensajes de JUEGO

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

        //#region Territorios China
        //public static List<Territorio> TerritoriosChina = new List<Territorio>(){
        //    new Territorio() { TerritorioId = 1, NombreTerritorio="Huijiang", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 2, NombreTerritorio="Tibet", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 3, NombreTerritorio="Qinghai", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 4, NombreTerritorio="Sichuan", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 5, NombreTerritorio="Yunnan", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 6, NombreTerritorio="Guizhou", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 7, NombreTerritorio="Uliassutai", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 8, NombreTerritorio="Mongolia", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 9, NombreTerritorio="Gansu", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 10, NombreTerritorio="Shaanxi", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 11, NombreTerritorio="Shanxi", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 12, NombreTerritorio="Hubei", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 13, NombreTerritorio="Heilongjiang", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 14, NombreTerritorio="Jilin", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 15, NombreTerritorio="Shengjing", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 16, NombreTerritorio="Zhili", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 17, NombreTerritorio="Shandong", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 18, NombreTerritorio="Menan", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 19, NombreTerritorio="Guangxi", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 20, NombreTerritorio="Hunan", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 21, NombreTerritorio="Ilangxi", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 22, NombreTerritorio="Fcohou", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 23, NombreTerritorio="Zhelang", NUnidadesDeplegadas = 0 },
        //    new Territorio() { TerritorioId = 24, NombreTerritorio="Anhu", NUnidadesDeplegadas = 0 }
        //};
        //#endregion
    }
}
