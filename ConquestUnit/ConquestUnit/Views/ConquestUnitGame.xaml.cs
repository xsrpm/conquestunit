﻿using DataModel;
using SynapseSDK;
using System;
using System.Linq;
using Util;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ConquestUnit.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ConquestUnitGame : Page
    {
        Windows.UI.Xaml.Shapes.Path[,] Territorio;
        Windows.UI.Xaml.Shapes.Path TerrSelec;

        Juego objJuego;

        Windows.UI.Xaml.Visibility[,] BlancoGridVisibilidad;

        public ConquestUnitGame()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;

            ////COMENTAR PARA PRUEBA
            if (e.Parameter != null)
            {
                objJuego = (Juego)e.Parameter;
            }
            else
            {
                Helper.MensajeOk("No se pudo iniciar el juego.");
            }
            ////

            ///DESCOMENTAR PARA PRUEBA
            /*
            objJuego = new Juego(Constantes.MAPA_CHINA);
            // INCIALIZACION DE PRUEBA --- el objeto juego viene de parametro "e"
            var jugador1 = new Jugador() { Conectado = true, Ip = "192.168.0.4", Nombre = "Roy" };
            var jugador2 = new Jugador() { Conectado = true, Ip = "192.168.0.6", Nombre = "Cesar" };
            var jugador3 = new Jugador() { Conectado = true, Ip = "192.168.0.8", Nombre = "Joel" };
            var jugador4 = new Jugador() { Conectado = true, Ip = "192.168.0.9", Nombre = "Christian" };
            objJuego.JugadoresConectados.Add(jugador1);
            objJuego.JugadoresConectados.Add(jugador2);
            objJuego.JugadoresConectados.Add(jugador3);
            objJuego.JugadoresConectados.Add(jugador4);
            // Definir los turnos de los jugadores,
            // los cuales será igual a la lista de jugadores de la mesa
            // pero en desorden
            GameLogic.LogicaInicio.InicializarTurnos(objJuego);
            // Repartir los territorios
            GameLogic.LogicaInicio.InicializarTerritorios(objJuego);
            GameLogic.LogicaInicio.RepartirTerritorio(objJuego);
            GameLogic.LogicaInicio.RepartirUnidadesEnTerritorios(objJuego);
            //Definir la fase inicial del juego
            GameLogic.LogicaInicio.IniciarVariablesInicioJuego(objJuego);
            */
            ///
            Inicializar();
            IniciarSDK();
        }

        public void Inicializar()
        {
            Territorio = new Windows.UI.Xaml.Shapes.Path[24, 4]
            {
                {Uliassutai,Qinghai,Tibet,null },{Huijiang,Sichuan,Yunnan,null },{Gansu,Sichuan,Tibet,Huijiang},
                {Gansu,Hubei,Yunnan,Tibet},{Sichuan,Guizhou,null,null },{Sichuan,Hunan,Guangxi,Yunnan },

                {null,Heilongjiang,Mongolia,Huijiang },{Uliassutai,Jilin,Gansu,Huijiang },{Mongolia,Shaanxi,Sichuan,Qinghai},
                {Mongolia,Shanxi,Sichuan,Gansu },{Mongolia,Zhili,Menan,Shaanxi },{Menan,Anhu,Hunan,Sichuan },

                {null,Jilin,Mongolia,Uliassutai },{Heilongjiang,null,Shengjing,Mongolia },{Jilin,Mongolia,Zhili,Zhili },
                {Mongolia,Shengjing,Shandong,Shanxi },{Zhili,null,Menan,Zhili },{Zhili,Shandong,Hubei,Shaanxi },

                {Guizhou,Hunan,null,Yunnan},{Hubei,Ilangxi,Guangxi,Guizhou },{Anhu,Fcohou,null,Hunan },
                {Zhelang,null,null,Ilangxi },{Anhu,null,Fcohou,Anhu },{Menan,Zhelang,Ilangxi,Hubei }
            };

            //Dibujar unidades y Cantidad de unidades
            for (int i = 0; i < objJuego.Territorios.Count; i++)
            {
                DibujarUnidadesTerritorioEnElMapa(objJuego.Territorios[i]);
            }
            ActualizarNumeroUnidadesInfo();
            ActualizarNumeroUnidadesParaDespliegue();

            objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
            objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;

            //blanco,equis,circulo,arriba,abajo
            BlancoGridVisibilidad = new Windows.UI.Xaml.Visibility[9, 5] {
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,0,0},
                {Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,Visibility.Collapsed,0,0},
                {0,0,Visibility.Collapsed,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,Visibility.Collapsed,Visibility.Collapsed},
                {0,0,0,0,0}
            };
            Seleccionar_Territorio(Huijiang);
        }

        private void MoverTerritorio(int direccion)
        {
            Windows.UI.Xaml.Shapes.Path Actual = TerrSelec;
            Windows.UI.Xaml.Shapes.Path Nuevo;
            Nuevo = Territorio[Convert.ToInt32(TerrSelec.Tag), direccion];
            if (Nuevo != null)
            {
                Seleccionar_Territorio(Nuevo);
            }
        }

        private void BtnArr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.ARRIBA);
        }

        private void BtnIzq_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.IZQUIERDA);
        }

        private void BtnDer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.DERECHA);
        }

        private void BtnAba_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.MoverTerritorio.ABAJO);
        }

        private void Seleccionar_Territorio(Windows.UI.Xaml.Shapes.Path territorio)
        {
            TerrSelec = territorio;
            Thickness Centro_TerrSelec = new Thickness
            (
                territorio.Margin.Left + territorio.Width / 2 - BlancoGrid.Width / 2,
                territorio.Margin.Top + territorio.Height / 2 - BlancoGrid.Height / 2,
                0,
                0
            );

            BlancoGrid.Margin = Centro_TerrSelec;
            MostrarBlanco();


            //Actual.Opacity = 0.5;
            //TerrSelec.Opacity = 1;
        }

        private void CambiarFase(int fase)
        {

        }

        public void MostrarBlanco()
        {
            Blanco.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 0];
            XButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 1];
            OButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 2];
            DownArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 3];
            UpArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 4];
        }

        private void XButtonGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        public void DibujarUnidadesTerritorioEnElMapa(Territorio territorio)
        {
            try
            {
                Uri uri = new Uri(territorio.ImagenUnidades);
                BitmapImage imagenUnidad = new BitmapImage(uri);

                Image image = (Image)this.FindName("Unidad" + territorio.NombreTerritorio);
                Ellipse elipse = (Ellipse)this.FindName("UnidadElipse" + territorio.NombreTerritorio);
                TextBlock texto = (TextBlock)this.FindName("UnidadCantidad" + territorio.NombreTerritorio);

                image.Source = imagenUnidad;
                elipse.Fill = Convertidor.GetSolidColorBrush(territorio.ColorUnidades);
                texto.Text = territorio.NUnidadesDeplegadas.ToString();
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                throw;
            }
        }

        public void ActualizarNumeroUnidadesInfo()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)this.FindName("UnidadCantidad" + (i + 1))).Text =
                    objJuego.Territorios.Where(x => x.IpJugadorPropietario.Equals(objJuego.JugadoresConectados[i].Ip)).Sum(x => x.NUnidadesDeplegadas).ToString();
            }
        }

        public void ActualizarNumeroUnidadesParaDespliegue()
        {
            GameLogic.DespliegueLogic.UnidadesDisponiblesDesplegarJugadorTurnoActual(objJuego);
            txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();
        }

        private void BotonA_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonB_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonY_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void BotonX_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (objJuego.AccionActual == Constantes.AccionJuego.DESPLEGAR)
            {
                objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas += 1;
                ((TextBlock)this.FindName("UnidadCantidad" + objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio)).Text =
                    objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
                ActualizarNumeroUnidadesInfo();
            }
        }

        private void MiMetodoReceptorMesaJuegoHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region El jugador ha presiona una tecla
                    if (strMensaje.Trim().Contains(Constantes.JugadorPresionaBoton))
                    {
                        // Se recibe la confirmación de parte de la mesa que se unido satisfactoriamente
                        var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                        //mensaje[0] => Acción (JugadorPresionaBoton)
                        //mensaje[1] => objJugador.Ip
                        //mensaje[2] => botonPresionado
                        if (mensaje.Length != 3)
                            return;

                        //Verificar que es el jugador en turno
                        //Si no está en turno, verificar que no está respondiendo pregunta
                        if (!objJuego.IpJugadorTurnoActual.Equals(mensaje[1]) && !objJuego.IpJugadorDefiende.Equals(mensaje[1]))
                        {
                            return;
                        }

                        //Verificar que boton ha presionado
                        int botonPresionado = int.Parse(mensaje[2]);

                        #region DESPLIEGUE
                        if (objJuego.FaseActual == Constantes.FaseJuego.DESPLIEGUE)
                        {
                            //Boton direccional
                            if (botonPresionado >= 0 && botonPresionado <= 3)
                            {
                                MoverTerritorio(botonPresionado);
                            }
                            else if (botonPresionado==Constantes.Controles.EQUIS)
                            {
                                //Verificar que el jugador tiene unidades para desplegar

                                //Verificar que el territorio perteneza al jugador
                                if (!objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                {
                                    return;
                                }

                                objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas += 1;
                                ((TextBlock)this.FindName("UnidadCantidad" + objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio)).Text =
                                    objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
                                objJuego.UnidadesDisponiblesParaDesplegar--;
                                ActualizarNumeroUnidadesInfo();
                                txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();

                                //Si el jugador se queda sin unidades para desplegar, cambiar fase y acción
                                if (objJuego.UnidadesDisponiblesParaDesplegar==0)
                                {
                                    objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    Helper.MensajeOk("Termino la fase de despliegue");
                                }
                            }
                            else if (botonPresionado == Constantes.Controles.CIRCULO)
                            {
                                //El jugador está pidiendo terminar la fase de Despliegue y pasar a Ataque
                                objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                Helper.MensajeOk("Solicitaste terminar la fase de despliegue");
                            }
                        }
                        #endregion
                        #region ATAQUE
                        else if (objJuego.FaseActual == Constantes.FaseJuego.ATAQUE)
                        {

                        }
                        #endregion
                        #region FORTIFICACION
                        else if (objJuego.FaseActual == Constantes.FaseJuego.FORTIFICACION)
                        {
                            //Boton direccional
                            if (botonPresionado > 0 && botonPresionado <= 3)
                            {
                                MoverTerritorio(botonPresionado);
                            }
                        }
                        #endregion
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                return;
            }
        }

        #region Conexion SynapseSDK
        private void IniciarSDK()
        {
            try
            {
                App.UIDispatcher = this.Dispatcher;
                App.objSDK = MainCore.getInstance(Constantes.MULTICAST_ADDRESS, Constantes.MULTICAST_SERVICE_PORT, Constantes.UNICAST_SERVICE_PORT, Constantes.STREAM_SERVICE_PORT, MiMetodoReceptorMesaJuego, Constantes.DELAY);
                int cont = 0;
                while (!App.objSDK.SocketIsConnected && cont < 3)
                {
                    App.objSDK.TearDownSockets();
                    App.objSDK.InitializeSockets();
                    cont++;
                }

                if (App.objSDK.SocketIsConnected)
                {
                    App.objSDK.setObjMetodoReceptorString = MiMetodoReceptorMesaJuego;
                }
                else
                {
                    //No hay conexión
                    string strMensaje = "Lo sentimos, no se pudo establecer la conexión vía Wi-Fi. Intente nuevamente.";
                    Helper.MensajeOk(strMensaje);
                    this.Frame.Navigate(typeof(SeleccionarRol));
                    return;
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }

        public async void MiMetodoReceptorMesaJuego(string strIp, string strMessage)
        {
            try
            {
                await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    MiMetodoReceptorMesaJuegoHelper(strIp, strMessage);
                });
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
            }
        }
        #endregion
    }
}