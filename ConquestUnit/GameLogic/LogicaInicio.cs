using DataModel;
using System.Collections.Generic;
using System.Linq;
using Util;

namespace GameLogic
{
    public static class LogicaInicio
    {
        public static void InicializarTurnos(Juego juego)
        {
            Helper.Shuffle<Jugador>(juego.JugadoresConectados);
            #region China
            if (juego.TipoMapa == Constantes.MAPA.CHINA)
            {
                if (juego.JugadoresConectados.Count >= 1)
                {
                    juego.JugadoresConectados[0].Color = Constantes.ColorJug1;
                    juego.JugadoresConectados[0].ColorPastel = Constantes.ColorPastelJug1;
                    juego.JugadoresConectados[0].ImagenUnidad = Constantes.UnidadJug1CHINA;
                    juego.JugadoresConectados[0].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug1CHINA;
                }
                if (juego.JugadoresConectados.Count >= 2)
                {
                    juego.JugadoresConectados[1].Color = Constantes.ColorJug2;
                    juego.JugadoresConectados[1].ColorPastel = Constantes.ColorPastelJug2;
                    juego.JugadoresConectados[1].ImagenUnidad = Constantes.UnidadJug2CHINA;
                    juego.JugadoresConectados[1].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug2CHINA;
                }
                if (juego.JugadoresConectados.Count >= 3)
                {
                    juego.JugadoresConectados[2].Color = Constantes.ColorJug3;
                    juego.JugadoresConectados[2].ColorPastel = Constantes.ColorPastelJug3;
                    juego.JugadoresConectados[2].ImagenUnidad = Constantes.UnidadJug3CHINA;
                    juego.JugadoresConectados[2].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug3CHINA;
                }
                if (juego.JugadoresConectados.Count >= 4)
                {
                    juego.JugadoresConectados[3].Color = Constantes.ColorJug4;
                    juego.JugadoresConectados[3].ColorPastel = Constantes.ColorPastelJug4;
                    juego.JugadoresConectados[3].ImagenUnidad = Constantes.UnidadJug4CHINA;
                    juego.JugadoresConectados[3].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug4CHINA;
                }
            }
            #endregion
            #region Roma
            else if (juego.TipoMapa == Constantes.MAPA.ROMA)
            {
                if (juego.JugadoresConectados.Count >= 1)
                {
                    juego.JugadoresConectados[0].Color = Constantes.ColorJug1;
                    juego.JugadoresConectados[0].ColorPastel = Constantes.ColorPastelJug1;
                    juego.JugadoresConectados[0].ImagenUnidad = Constantes.UnidadJug1ROMA;
                    juego.JugadoresConectados[0].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug1ROMA;
                }
                if (juego.JugadoresConectados.Count >= 2)
                {
                    juego.JugadoresConectados[1].Color = Constantes.ColorJug2;
                    juego.JugadoresConectados[1].ColorPastel = Constantes.ColorPastelJug2;
                    juego.JugadoresConectados[1].ImagenUnidad = Constantes.UnidadJug2ROMA;
                    juego.JugadoresConectados[1].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug2ROMA;
                }
                if (juego.JugadoresConectados.Count >= 3)
                {
                    juego.JugadoresConectados[2].Color = Constantes.ColorJug3;
                    juego.JugadoresConectados[2].ColorPastel = Constantes.ColorPastelJug3;
                    juego.JugadoresConectados[2].ImagenUnidad = Constantes.UnidadJug3ROMA;
                    juego.JugadoresConectados[2].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug3ROMA;
                }
                if (juego.JugadoresConectados.Count >= 4)
                {
                    juego.JugadoresConectados[3].Color = Constantes.ColorJug4;
                    juego.JugadoresConectados[3].ColorPastel = Constantes.ColorPastelJug4;
                    juego.JugadoresConectados[3].ImagenUnidad = Constantes.UnidadJug4ROMA;
                    juego.JugadoresConectados[3].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug4ROMA;
                }
            }
            #endregion
            #region NorteAmerica
            else if (juego.TipoMapa == Constantes.MAPA.NORTEAMERICA)
            {
                if (juego.JugadoresConectados.Count >= 1)
                {
                    juego.JugadoresConectados[0].Color = Constantes.ColorJug1;
                    juego.JugadoresConectados[0].ColorPastel = Constantes.ColorPastelJug1;
                    juego.JugadoresConectados[0].ImagenUnidad = Constantes.UnidadJug1NORTEAMERICA;
                    juego.JugadoresConectados[0].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug1NORTEAMERICA;
                }
                if (juego.JugadoresConectados.Count >= 2)
                {
                    juego.JugadoresConectados[1].Color = Constantes.ColorJug2;
                    juego.JugadoresConectados[1].ColorPastel = Constantes.ColorPastelJug2;
                    juego.JugadoresConectados[1].ImagenUnidad = Constantes.UnidadJug2NORTEAMERICA;
                    juego.JugadoresConectados[1].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug2NORTEAMERICA;
                }
                if (juego.JugadoresConectados.Count >= 3)
                {
                    juego.JugadoresConectados[2].Color = Constantes.ColorJug3;
                    juego.JugadoresConectados[2].ColorPastel = Constantes.ColorPastelJug3;
                    juego.JugadoresConectados[2].ImagenUnidad = Constantes.UnidadJug3NORTEAMERICA;
                    juego.JugadoresConectados[2].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug3NORTEAMERICA;
                }
                if (juego.JugadoresConectados.Count >= 4)
                {
                    juego.JugadoresConectados[3].Color = Constantes.ColorJug4;
                    juego.JugadoresConectados[3].ColorPastel = Constantes.ColorPastelJug4;
                    juego.JugadoresConectados[3].ImagenUnidad = Constantes.UnidadJug4NORTEAMERICA;
                    juego.JugadoresConectados[3].ImagenUnidadAgrupadora = Constantes.UnidadAgrupadoraJug4NORTEAMERICA;
                }
            }
            #endregion
        }

        public static void InicializarTerritorios(Juego juego)
        {
            #region Territorios China
            if (juego.TipoMapa == Constantes.MAPA.CHINA)
            {
                juego.Territorios = new List<Territorio>(){
                    new Territorio() { TerritorioId = 0, NombreTerritorio="Huijiang", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 1, NombreTerritorio="Tibet", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 2, NombreTerritorio="Qinghai", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 3, NombreTerritorio="Sichuan", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 4, NombreTerritorio="Yunnan", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 5, NombreTerritorio="Guizhou", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 6, NombreTerritorio="Uliassutai", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 7, NombreTerritorio="Mongolia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 8, NombreTerritorio="Gansu", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 9, NombreTerritorio="Shaanxi", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 10, NombreTerritorio="Shanxi", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 11, NombreTerritorio="Hubei", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 12, NombreTerritorio="Heilongjiang", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 13, NombreTerritorio="Jilin", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 14, NombreTerritorio="Shengjing", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 15, NombreTerritorio="Zhili", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 16, NombreTerritorio="Shandong", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 17, NombreTerritorio="Menan", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 18, NombreTerritorio="Guangxi", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 19, NombreTerritorio="Hunan", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 20, NombreTerritorio="Ilangxi", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 21, NombreTerritorio="Fcohou", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 22, NombreTerritorio="Zhelang", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 23, NombreTerritorio="Anhu", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 }
                };
            }
            #endregion
            #region Territorios Roma
            if (juego.TipoMapa == Constantes.MAPA.ROMA)
            {
                juego.Territorios = new List<Territorio>(){
                    new Territorio() { TerritorioId = 0, NombreTerritorio="Lusitania", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 1, NombreTerritorio="Augusta", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 2, NombreTerritorio="Aquitania", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 3, NombreTerritorio="Alpina", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 4, NombreTerritorio="Frisia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 5, NombreTerritorio="Flavia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 6, NombreTerritorio="Tingitana", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 7, NombreTerritorio="Carthago", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 8, NombreTerritorio="Libya", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 9, NombreTerritorio="Cyrene", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 10, NombreTerritorio="Arcadia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 11, NombreTerritorio="Thebaida", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 12, NombreTerritorio="Lydia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 13, NombreTerritorio="Capadocia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 14, NombreTerritorio="Armenia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 15, NombreTerritorio="Mesopotania", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 16, NombreTerritorio="Siria", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 17, NombreTerritorio="Salutaris", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 18, NombreTerritorio="Sardinia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 19, NombreTerritorio="Roma", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 20, NombreTerritorio="Dalmatia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 21, NombreTerritorio="Moesia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 22, NombreTerritorio="Pannonia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 23, NombreTerritorio="Peloponense", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 }
                };
            }
            #endregion
            #region Territorios Roma
            if (juego.TipoMapa == Constantes.MAPA.NORTEAMERICA)
            {
                juego.Territorios = new List<Territorio>(){
                    new Territorio() { TerritorioId = 0, NombreTerritorio="Doyon", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 1, NombreTerritorio="PendArtica", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 2, NombreTerritorio="Yukon", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 3, NombreTerritorio="TerritNores", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 4, NombreTerritorio="Columbia", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },
                    new Territorio() { TerritorioId = 5, NombreTerritorio="Alberta", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION3 },

                    new Territorio() { TerritorioId = 6, NombreTerritorio="California", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 7, NombreTerritorio="Utah", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 8, NombreTerritorio="Sonora", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 9, NombreTerritorio="Monterrey", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 10, NombreTerritorio="Guerrero", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },
                    new Territorio() { TerritorioId = 11, NombreTerritorio="Yucatan", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION4 },

                    new Territorio() { TerritorioId = 12, NombreTerritorio="Montana", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 13, NombreTerritorio="Dakota", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 14, NombreTerritorio="Texas", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 15, NombreTerritorio="Ohio", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 16, NombreTerritorio="Kentucky", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },
                    new Territorio() { TerritorioId = 17, NombreTerritorio="NuevaYork", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION1 },

                    new Territorio() { TerritorioId = 18, NombreTerritorio="Labrador", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 19, NombreTerritorio="Quebec", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 20, NombreTerritorio="Ontario", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 21, NombreTerritorio="Manitoba", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 22, NombreTerritorio="Numavut", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 },
                    new Territorio() { TerritorioId = 23, NombreTerritorio="Victoria", NUnidadesDeplegadas = 0, NroRegion= Constantes.Region.REGION2 }
                };
            }
            #endregion
        }

        public static void RepartirTerritorio(Juego juego)
        {
            var cantidadJugadores = juego.JugadoresConectados.Count;
            int maxTerritoriosIniciales = (cantidadJugadores == 2 ? 12 : (cantidadJugadores == 3 ? 8 : 6));

            int siguiente;

            int territoriosJug1 = 0;
            int territoriosJug2 = 0;
            int territoriosJug3 = 0;
            int territoriosJug4 = 0;
            if (cantidadJugadores == 2)
            {
                // 2 jugadores => 12 territorios
                for (int i = 0; i < juego.Territorios.Count; i++)
                {
                    while (true)
                    {
                        siguiente = Helper.IntUtil.Random(1, 101);
                        if (siguiente % 2 == 0)
                        {
                            if (territoriosJug1 < maxTerritoriosIniciales)
                            {
                                siguiente = 1;
                                break;
                            }
                        }
                        else
                        {
                            if (territoriosJug2 < maxTerritoriosIniciales)
                            {
                                siguiente = 2;
                                break;
                            }
                        }
                    }
                    switch (siguiente)
                    {
                        case 1:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[0].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[0].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[0].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[0].ImagenUnidad;
                            territoriosJug1++;
                            break;
                        case 2:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[1].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[1].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[1].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[1].ImagenUnidad;
                            territoriosJug2++;
                            break;
                    }
                }
            }
            else if (cantidadJugadores == 3)
            {
                // 3 jugadores => 8 territorios
                for (int i = 0; i < juego.Territorios.Count; i++)
                {
                    while (true)
                    {
                        siguiente = Helper.IntUtil.Random(1, 4);
                        if (siguiente == 1 && territoriosJug1 < maxTerritoriosIniciales)
                        {
                            break;
                        }
                        else
                        {
                            siguiente = Helper.IntUtil.Random(2, 4);
                        }

                        if (siguiente == 2 && territoriosJug2 < maxTerritoriosIniciales)
                        {
                            break;
                        }
                        else if(siguiente == 3 || territoriosJug3 < maxTerritoriosIniciales)
                        {
                            siguiente = 3;
                            break;
                        }
                        else if (territoriosJug1 < maxTerritoriosIniciales)
                        {
                            siguiente = 1;
                            break;
                        }
                    }
                    switch (siguiente)
                    {
                        case 1:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[0].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[0].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[0].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[0].ImagenUnidad;
                            territoriosJug1++;
                            break;
                        case 2:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[1].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[1].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[1].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[1].ImagenUnidad;
                            territoriosJug2++;
                            break;
                        case 3:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[2].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[2].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[2].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[2].ImagenUnidad;
                            territoriosJug3++;
                            break;
                    }
                }
            }
            else if (cantidadJugadores == 4)
            {
                // 4 jugadores => 6 territorios
                for (int i = 0; i < juego.Territorios.Count; i++)
                {
                    while (true)
                    {
                        siguiente = Helper.IntUtil.Random(1, 5);
                        if (siguiente == 1 && territoriosJug1 < maxTerritoriosIniciales)
                        {
                            break;
                        }
                        else
                        {
                            siguiente = Helper.IntUtil.Random(2, 5);
                        }

                        if (siguiente == 2 && territoriosJug2 < maxTerritoriosIniciales)
                        {
                            break;
                        }
                        else
                        {
                            siguiente = Helper.IntUtil.Random(3, 5);
                        }



                        if (siguiente == 3 && territoriosJug3 < maxTerritoriosIniciales)
                        {
                            siguiente = 3;
                            break;
                        }
                        else if (siguiente == 4 || territoriosJug4 < maxTerritoriosIniciales)
                        {
                            siguiente = 4;
                            break;
                        }
                        else if (territoriosJug1 < maxTerritoriosIniciales)
                        {
                            siguiente = 1;
                            break;
                        }
                    }
                    switch (siguiente)
                    {
                        case 1:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[0].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[0].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[0].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[0].ImagenUnidad;
                            territoriosJug1++;
                            break;
                        case 2:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[1].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[1].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[1].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[1].ImagenUnidad;
                            territoriosJug2++;
                            break;
                        case 3:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[2].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[2].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[2].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[2].ImagenUnidad;
                            territoriosJug3++;
                            break;
                        case 4:
                            juego.Territorios[i].ColorUnidades = juego.JugadoresConectados[3].Color;
                            juego.Territorios[i].IpJugadorPropietario = juego.JugadoresConectados[3].Ip;
                            juego.Territorios[i].NombreJugadorPropietario = juego.JugadoresConectados[3].Nombre;
                            juego.Territorios[i].ImagenUnidades = juego.JugadoresConectados[3].ImagenUnidad;
                            territoriosJug4++;
                            break;
                    }
                }
            }
        }

        public static void RepartirUnidadesEnTerritorios(Juego juego)
        {
            var cantidadJugadores = juego.JugadoresConectados.Count;
            int maxUnidadesIniciales = (cantidadJugadores == 2 ? 23 : (cantidadJugadores == 3 ? 20 : 17));
            for (int i = 0; i < juego.Territorios.Count; i++)
            {
                var auxIpJugadorPropietario = juego.Territorios[i].IpJugadorPropietario;
                int unidadesAgregar = Helper.IntUtil.Random(1, 3);
                int unidadesActuales = juego.Territorios.Where(x => x.IpJugadorPropietario.Equals(auxIpJugadorPropietario)).Sum(x => x.NUnidadesDeplegadas);
                if (unidadesActuales + unidadesAgregar < maxUnidadesIniciales)
                {
                    juego.Territorios[i].NUnidadesDeplegadas = juego.Territorios[i].NUnidadesDeplegadas + unidadesAgregar;
                }
                else
                {
                    juego.Territorios[i].NUnidadesDeplegadas = maxUnidadesIniciales - unidadesActuales;
                }
            }
            // Validar que todos los jugadores tengan el maxUnidadesIniciales
            for (int i = 0; i < juego.JugadoresConectados.Count; i++)
            {
                var aux = juego.Territorios.Where(x => x.IpJugadorPropietario.Equals(juego.JugadoresConectados[i].Ip)).Sum(x => x.NUnidadesDeplegadas);
                if (aux < maxUnidadesIniciales)
                {
                    for (int k = 0; k < juego.Territorios.Count; k++)
                    {
                        if (juego.Territorios[k].IpJugadorPropietario.Equals(juego.JugadoresConectados[i].Ip))
                        {
                            juego.Territorios[k].NUnidadesDeplegadas += 1;
                        }
                        aux = juego.Territorios.Where(x => x.IpJugadorPropietario.Equals(juego.JugadoresConectados[i].Ip)).Sum(x => x.NUnidadesDeplegadas);
                        if (aux == maxUnidadesIniciales)
                        {
                            break;
                        }
                        else
                        {
                            if (k == juego.Territorios.Count -1)
                            {
                                k = 0;
                            }
                        }
                    }
                }
            }
        }

        public static void IniciarVariablesInicioJuego(Juego objJuego)
        {
            objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
            objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;
            objJuego.TurnoActual = 0;
            objJuego.IpJugadorTurnoActual = objJuego.JugadoresConectados[0].Ip;
            objJuego.IpJugadorDefiende = "";
            //for (int i = 0; i < objJuego.Territorios.Count-1; i++)
            //{
            //    objJuego.Territorios[i].ColorUnidades = objJuego.JugadorTurnoActual().Color;
            //    objJuego.Territorios[i].ImagenUnidades = objJuego.JugadorTurnoActual().ImagenUnidad;
            //    objJuego.Territorios[i].IpJugadorPropietario = objJuego.JugadorTurnoActual().Ip;
            //    objJuego.Territorios[i].NombreJugadorPropietario = objJuego.JugadorTurnoActual().Nombre;
            //}
        }
    }
}
