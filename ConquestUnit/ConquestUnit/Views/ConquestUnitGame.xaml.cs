using DataModel;
using SynapseSDK;
using System;
using System.Linq;
using System.Threading;
using Util;
using Windows.Graphics.Display;
using Windows.Networking;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        private Timer timerInicioBatalla;
        private int? contadorInicioTimer;
        private bool inicioBatalla;
        private bool esPrimeraPregunta;
        private Timer timerBatallaPrimeraPregunta;
        private int? contadorSegDuracionTimer;
        private Timer timerBatallaSegundaPregunta;
        private int respuestaAtacante;
        private int respuestaDefensor;

        private int puntosAtacante;
        private int puntosDefensor;

        public ConquestUnitGame()
        {
            this.InitializeComponent();

            timerInicioBatalla = new Timer(timerInicioBatallaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerBatallaPrimeraPregunta = new Timer(timerBatallaPrimeraPreguntaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerBatallaSegundaPregunta = new Timer(timerBatallaSegundaPreguntaCallback, null, Timeout.Infinite, Timeout.Infinite);
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
            var jugador1 = new Jugador() { Conectado = true, Ip = "192.168.1.36", Nombre = "Roy" };
            var jugador2 = new Jugador() { Conectado = true, Ip = "192.168.1.38", Nombre = "Cesar" };
            var jugador3 = new Jugador() { Conectado = true, Ip = "192.168.0.8", Nombre = "Joel" };
            var jugador4 = new Jugador() { Conectado = true, Ip = "192.168.0.9", Nombre = "Christian" };
            objJuego.JugadoresConectados.Add(jugador1);
            objJuego.JugadoresConectados.Add(jugador2);
            //objJuego.JugadoresConectados.Add(jugador3);
            //objJuego.JugadoresConectados.Add(jugador4);
            // Definir los turnos de los jugadores,
            // los cuales será igual a la lista de jugadores de la mesa
            // pero en desorden
            GameLogic.LogicaInicio.InicializarTurnos(objJuego);
            // Repartir los territorios
            GameLogic.LogicaInicio.InicializarTerritorios(objJuego);
            GameLogic.LogicaInicio.RepartirTerritorio(objJuego);
            GameLogic.LogicaInicio.RepartirUnidadesEnTerritorios(objJuego);
            //Notificar a los jugadores el inicio del juego y quien comienza
            foreach (var item in objJuego.JugadoresConectados)
                await App.objSDK.UnicastPing(new HostName(item.Ip),
                            Constantes.MesaIndicaJuegoInicia + Constantes.SEPARADOR +
                            objJuego.JugadoresConectados[0].Ip);
            //Definir la fase inicial del juego
            GameLogic.LogicaInicio.IniciarVariablesInicioJuego(objJuego);
            */
            ///
            Inicializar();
            IniciarSDK();
        }

        public void Inicializar()
        {
            BatallaGrid.Visibility = Visibility.Collapsed;
            //Inicializar Mapa
            Territorio = new Windows.UI.Xaml.Shapes.Path[24, 8]
            {
                {Uliassutai,Uliassutai,Gansu,Qinghai,Tibet,null,null,null},//Huijiang
                {Huijiang,Qinghai,Sichuan,Yunnan,null,null,null,null },//Tibet
                {Gansu,Gansu,Gansu,Sichuan,Tibet,Tibet,Tibet,Huijiang},//Qinghai
                {Gansu,Shaanxi,Hubei,Guizhou,Yunnan,Yunnan,Tibet,Qinghai},//Sichuan
                {Sichuan,Sichuan,Guizhou,Guangxi,null,null,null,Tibet },//Yunnan
                {Sichuan,Sichuan,Hunan,Guangxi,Guangxi,Yunnan,Yunnan,Sichuan },//Guizhou

                {null,Heilongjiang,Heilongjiang,Mongolia,Mongolia,Huijiang,Huijiang,null },//Uliassutai
                {Uliassutai,Heilongjiang,Zhili,Shanxi,Shaanxi,Gansu,Uliassutai,Uliassutai },//Mongolia
                {Mongolia,Mongolia,Shaanxi,Sichuan,Qinghai,Qinghai,Qinghai,Huijiang},//Gansu
                {Mongolia,Shanxi,Menan,Hubei,Sichuan,Sichuan,Gansu,Gansu },//Shaanxi
                {Mongolia,Zhili,Zhili,Menan,Menan,Shaanxi,Shaanxi,Mongolia },//Shanxi
                { Menan,Anhu,Anhu,Ilangxi,Hunan,Sichuan,Sichuan,Shaanxi },//Hubei

                { null,null,Jilin,Jilin,Mongolia,Uliassutai,Uliassutai,null },//Heilongjiang
                { Heilongjiang,null,null,null,null,Shengjing,Mongolia,Heilongjiang },//Jilin
                { Mongolia,Jilin,null,null,null,null,Zhili,Zhili },//Shengjing
                { Mongolia,Mongolia,Shengjing,Shandong,Menan,Shaanxi,Shanxi,Mongolia },//Zhili
                { null,null,null,null,null,Menan,Zhili,Zhili },//Shandong
                { Zhili,Shandong,Anhu,Anhu,Hubei,Hubei,Shaanxi,Shanxi },//Menan

                { Guizhou,Hunan,null,null,null,null,Yunnan,Guizhou},//Guangxi
                { Hubei,Hubei,Ilangxi,null,Guangxi,Guangxi,Guizhou,Sichuan },//Hunan
                { Anhu,Zhelang,Fcohou,Fcohou,null,null,Hunan,Hubei },//Ilangxi
                { Zhelang,Zhelang,null,null,null,null,Ilangxi,Ilangxi },//Fcohou
                { null,null,null,null,Fcohou,Fcohou,Ilangxi,Anhu },//Zhelang
                { null,null,null,Zhelang,Ilangxi,Hubei,Menan,Menan }//Anhu
            };
            //Dibujar unidades y Cantidad de unidades
            DibujarJugadores();
            DibujarUnidadesTerritorioEnElMapa();
            ActualizarNumeroTerritoriosInfo();
            ActualizarNumeroContinentesInfo();
            ActualizarNumeroUnidadesParaDespliegue();

            btnFaseDespliegue.IsEnabled = true;
            btnFaseAtaque.IsEnabled = false;
            btnFaseFortificacion.IsEnabled = false;

            //Cursor
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

        public async void DibujarJugadores()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)FindName("Jugador" + (i + 1) + "Nombre")).Text = objJuego.JugadoresConectados[i].Nombre;
                if (objJuego.JugadoresConectados[i].Imagen != null)
                {
                    BitmapImage bimgBitmapImage = new BitmapImage();
                    IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(objJuego.JugadoresConectados[i].Imagen);
                    bimgBitmapImage.SetSource(fileStream);
                    ((Image)FindName("Jugador" + (i + 1) + "Imagen")).Source = bimgBitmapImage;
                }
            }
            if (objJuego.JugadoresConectados.Count <= 3)
            {
                UnidadGrid4.Visibility = Visibility.Collapsed;
            }
            if (objJuego.JugadoresConectados.Count <= 2)
            {
                UnidadGrid3.Visibility = Visibility.Collapsed;
            }
        }

        public void DibujarUnidadesTerritorioEnElMapa()
        {
            try
            {
                for (int i = 0; i < objJuego.Territorios.Count; i++)
                {
                    var territorio = objJuego.Territorios[i];
                    Uri uri = new Uri(territorio.ImagenUnidades);
                    BitmapImage imagenUnidad = new BitmapImage(uri);

                    Image image = (Image)this.FindName("Unidad" + territorio.NombreTerritorio);
                    Ellipse elipse = (Ellipse)this.FindName("UnidadElipse" + territorio.NombreTerritorio);
                    TextBlock texto = (TextBlock)this.FindName("UnidadCantidad" + territorio.NombreTerritorio);

                    image.Source = imagenUnidad;
                    elipse.Fill = Convertidor.GetSolidColorBrush(territorio.ColorUnidades);
                    texto.Text = territorio.NUnidadesDeplegadas.ToString();
                }
            }
            catch (Exception ex)
            {
                Helper.MensajeOk(ex.Message);
                throw;
            }
        }

        public void ActualizarNumeroTerritoriosInfo()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)this.FindName("Jugador" + (i + 1) + "Territorios")).Text =
                    GameLogic.HelperLogic.NroTerritoriosJugador(objJuego, objJuego.JugadoresConectados[i].Ip).ToString();
            }
        }

        public void ActualizarNumeroContinentesInfo()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)this.FindName("Jugador" + (i + 1) + "Continentes")).Text =
                    GameLogic.HelperLogic.NroContinentesJugador(objJuego, objJuego.JugadoresConectados[i].Ip).ToString();
            }
        }

        public void ActualizarNumeroUnidadesParaDespliegue()
        {
            GameLogic.DespliegueLogic.UnidadesDisponiblesDesplegarJugadorTurnoActual(objJuego);
            txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();
        }

        //Cuando se muestra el Grid de Ataque
        public async void InicializarGridBatallaAtaque()
        {
            //Area combo de Ataque
            txtComboAtk.Text = "1";
            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            if (objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas > 1 && objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas != 2)
            {
                RightArrowBatallaGrid.Visibility = Visibility.Visible;
            }
            else
            {
                RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
            }
            XButtonGrid_Copy.Visibility = Visibility.Visible;
            AtacarBatallaTXT.Visibility = Visibility.Visible;
            //Area Pregunta
            CabeceraGrid.Visibility = Visibility.Visible;
            PreguntaGrid.Visibility = Visibility.Collapsed;
            MoverTropasGrid.Visibility = Visibility.Collapsed;
            txtTimerOpciones.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Visibility = Visibility.Collapsed;
            Opcion2Canvas.Visibility = Visibility.Collapsed;
            Opcion3Canvas.Visibility = Visibility.Collapsed;
            Opcion4Canvas.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion2Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion3Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion4Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion1Jugador1.Visibility = Visibility.Collapsed;
            Opcion2Jugador1.Visibility = Visibility.Collapsed;
            Opcion3Jugador1.Visibility = Visibility.Collapsed;
            Opcion4Jugador1.Visibility = Visibility.Collapsed;
            Opcion1Jugador2.Visibility = Visibility.Collapsed;
            Opcion2Jugador2.Visibility = Visibility.Collapsed;
            Opcion3Jugador2.Visibility = Visibility.Collapsed;
            Opcion4Jugador2.Visibility = Visibility.Collapsed;
            //Area Atacante
            var atacante = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorTurnoActual).First();
            PanelMensajeJugadorAtacante.Visibility = Visibility.Collapsed;
            txtCancelarJugadorAtacante.Visibility = Visibility.Collapsed;
            imgCancelarJugadorAtacante.Visibility = Visibility.Collapsed;
            AtacanteNombreTXT.Text = atacante.Nombre;
            InvasorFondo.Fill = Convertidor.GetSolidColorBrush(atacante.Color);
            if (atacante.Imagen != null)
            {
                BitmapImage bimgBitmapImage = new BitmapImage();
                IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(atacante.Imagen);
                bimgBitmapImage.SetSource(fileStream);
                InvasorFoto.Source = bimgBitmapImage;
            }
            NTropasInvasorTXT.Text = objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas.ToString();
            InvasorPuntosAtaque1TXT.Text = "0";
            InvasorPuntosAtaque2TXT.Text = "(0)";
            InvasorPuntosAtaque2TXT.Visibility = Visibility.Collapsed;
            //Area Defensor
            var defensor = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorDefiende).First();
            PanelMensajeJugador2.Visibility = Visibility.Collapsed;
            DefensorNombreTXT.Text = defensor.Nombre;
            DefensorFondo.Fill = Convertidor.GetSolidColorBrush(defensor.Color);
            if (defensor.Imagen != null)
            {
                BitmapImage bimgBitmapImage = new BitmapImage();
                IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(defensor.Imagen);
                bimgBitmapImage.SetSource(fileStream);
                DefensorFoto.Source = bimgBitmapImage;
            }
            NTropasDefensorTXT.Text = objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas.ToString();
            InvasorPuntosDefensaTXT.Text = "0";
        }

        //Cuando inicia la batalla, se muestra la pregunta
        public void IniciarBatalla()
        {
            esPrimeraPregunta = true;

            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
            XButtonGrid_Copy.Visibility = Visibility.Collapsed;
            AtacarBatallaTXT.Visibility = Visibility.Collapsed;

            PreguntaGrid.Visibility = Visibility.Visible;
            txtTimerOpciones.Visibility = Visibility.Visible;
            puntosAtacante = puntosDefensor = 0;
            contadorInicioTimer = null;
            contadorSegDuracionTimer = null;
            inicioBatalla = false;
            respuestaAtacante = respuestaDefensor = -1;
            CargarNuevaPregunta();
            timerInicioBatalla.Change(0, 1000 * 1);
        }

        // MOVER TROPASSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS
        public void IniciarMoverTropas()
        {
            PanelMensajeJugadorAtacante.Visibility = Visibility.Collapsed;
            //Mostrar grilla de mover tropas!!!!!!!!!!!!!!!!!!!!!!!
            CabeceraGrid.Visibility = Visibility.Collapsed;
            PreguntaGrid.Visibility = Visibility.Collapsed;
            MoverTropasGrid.Visibility = Visibility.Visible;
        }

        public async void timerInicioBatallaCallback(object sender)
        {
            if (contadorInicioTimer == null)
            {
                contadorInicioTimer = 3;
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // do some work on UI here;
                if (contadorInicioTimer == 3)
                {
                    txtTimerOpciones.Text = "Preparados...";
                }
                else if (contadorInicioTimer == 2)
                {
                    txtTimerOpciones.Text = "Listos...";
                }
                else if (contadorInicioTimer == 1)
                {
                    txtTimerOpciones.Text = "A responder...!!!";
                }
                if (contadorInicioTimer <= 0)
                {
                    timerInicioBatalla.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorInicioTimer = null;
                    MostrarOpciones();
                }
                else
                {
                    contadorInicioTimer--;
                }
            });
        }

        public async void timerBatallaPrimeraPreguntaCallback(object sender)
        {
            if (contadorSegDuracionTimer == null)
            {
                contadorSegDuracionTimer = 10;
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // do some work on UI here;
                txtTimerOpciones.Text = contadorSegDuracionTimer.ToString();
                contadorSegDuracionTimer--;

                if (contadorSegDuracionTimer < 0 || (respuestaAtacante != -1 && respuestaDefensor != -1))
                {
                    inicioBatalla = false;
                    timerBatallaPrimeraPregunta.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorSegDuracionTimer = null;
                    //Verificar quien tuvo la respuesta correcta y acreditar los puntos
                    for (int i = 0; i < objJuego.Opciones.Count; i++)
                    {
                        if (objJuego.Opciones[i].EsRespuesta)
                        {
                            ((Grid)FindName("Opcion" + (i + 1) + "Canvas")).Background = Convertidor.GetSolidColorBrush(Constantes.ColorCorrecto);
                            if (respuestaAtacante != i)
                            {
                                puntosAtacante = 0;
                            }
                            if (respuestaDefensor != i)
                            {
                                puntosDefensor = 0;
                            }
                        }
                        else
                        {
                            if (respuestaAtacante == i || respuestaDefensor == i)
                            {
                                ((Grid)FindName("Opcion" + (i + 1) + "Canvas")).Background = Convertidor.GetSolidColorBrush(Constantes.ColorIncorrecto);
                            }
                        }
                    }
                    //Mostrar puntuaciones de los jugadores
                    InvasorPuntosAtaque1TXT.Text = puntosAtacante.ToString();
                    InvasorPuntosDefensaTXT.Text = puntosDefensor.ToString();

                    //Desactivar los controles del defensor
                    App.objSDK.UnicastPing(new HostName(objJuego.IpJugadorDefiende),
                        Constantes.MesaConumicaDESHABILITARControles);

                    //Gano el atacante
                    if (puntosAtacante > puntosDefensor)
                    {
                        objJuego.Territorios[objJuego.TerritorioDestinoAtaque.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioDestinoAtaque.NombreTerritorio)).Text = objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas.ToString();
                        //Mostrar el mensaje de victoria del Atacante
                        PanelMensajeJugadorAtacante.Visibility = Visibility.Visible;
                        PanelMensajeJugador2.Visibility = Visibility.Visible;
                        txtMensajeJugador2.Text = Constantes.MensajesResultadoBatalla.PierdeDefensor;
                        if (objJuego.Territorios[objJuego.TerritorioDestinoAtaque.TerritorioId].NUnidadesDeplegadas == 0)
                        {
                            txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.GanaAtacanteTerritorio;
                            objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_INICIO_MOVERTROPAS;
                        }
                        else
                        {
                            txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.GanaAtacante;
                            objJuego.AccionActual = Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE;
                        }
                    }
                    //Gano el defensor
                    else
                    {
                        //Mostrar el mensaje que el atacante puede responder 1 pregunta más
                        PanelMensajeJugadorAtacante.Visibility = Visibility.Visible;
                        txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.AtacanteResponderaSegundaPregunta;
                        objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_INICIO_SEGUNDA_PREGUNTA;
                    }
                }
            });
        }

        public async void timerBatallaSegundaPreguntaCallback(object sender)
        {
            if (contadorSegDuracionTimer == null)
            {
                contadorSegDuracionTimer = 10;
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // do some work on UI here;
                txtTimerOpciones.Text = contadorSegDuracionTimer.ToString();
                contadorSegDuracionTimer--;

                if (contadorSegDuracionTimer < 0 || respuestaAtacante != -1)
                {
                    timerBatallaSegundaPregunta.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorSegDuracionTimer = null;
                    //Verificar quien tuvo la respuesta correcta y acreditar los puntos
                    for (int i = 0; i < objJuego.Opciones.Count; i++)
                    {
                        if (objJuego.Opciones[i].EsRespuesta)
                        {
                            ((Grid)FindName("Opcion" + (i + 1) + "Canvas")).Background = Convertidor.GetSolidColorBrush(Constantes.ColorCorrecto);
                            if (respuestaAtacante != i)
                            {
                                puntosAtacante = 0;
                            }
                        }
                        else
                        {
                            if (respuestaAtacante == i)
                            {
                                ((Grid)FindName("Opcion" + (i + 1) + "Canvas")).Background = Convertidor.GetSolidColorBrush(Constantes.ColorIncorrecto);
                            }
                        }
                    }
                    //Mostrar puntuaciones de los jugadores
                    InvasorPuntosAtaque1TXT.Text = puntosAtacante.ToString();

                    //Gano el atacante
                    if (puntosAtacante > puntosDefensor)
                    {
                        objJuego.Territorios[objJuego.TerritorioDestinoAtaque.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioDestinoAtaque.NombreTerritorio)).Text = objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas.ToString();
                        //Mostrar el mensaje de victoria del Atacante
                        PanelMensajeJugadorAtacante.Visibility = Visibility.Visible;
                        PanelMensajeJugador2.Visibility = Visibility.Visible;
                        txtMensajeJugador2.Text = Constantes.MensajesResultadoBatalla.PierdeDefensor;
                        if (objJuego.Territorios[objJuego.TerritorioDestinoAtaque.TerritorioId].NUnidadesDeplegadas == 0)
                        {
                            txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.GanaAtacanteTerritorio;
                            objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_INICIO_MOVERTROPAS;
                        }
                        else
                        {
                            txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.GanaAtacante;
                            objJuego.AccionActual = Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE;
                        }
                    }
                    //Gano el defensor
                    else
                    {
                        objJuego.Territorios[objJuego.TerritorioOrigenAtaque.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioOrigenAtaque.NombreTerritorio)).Text = objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas.ToString();
                        //Mostrar el mensaje de victoria del DEFENSOR
                        PanelMensajeJugadorAtacante.Visibility = Visibility.Visible;
                        txtMensajeJugadorAtacante.Text = Constantes.MensajesResultadoBatalla.PierdeAtacante;
                        PanelMensajeJugador2.Visibility = Visibility.Visible;
                        txtMensajeJugador2.Text = Constantes.MensajesResultadoBatalla.GanaDefensor;
                        if (objJuego.Territorios[objJuego.TerritorioOrigenAtaque.TerritorioId].NUnidadesDeplegadas <= 1)
                        {
                            objJuego.AccionActual = Constantes.AccionJuego.TERMINAR_ATAQUE;
                        }
                        else
                        {
                            objJuego.AccionActual = Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE;
                        }
                    }
                }
            });
        }

        public void MostrarOpciones()
        {
            Opcion1Canvas.Visibility = Visibility.Visible;
            Opcion2Canvas.Visibility = Visibility.Visible;
            Opcion3Canvas.Visibility = Visibility.Visible;
            Opcion4Canvas.Visibility = Visibility.Visible;
            inicioBatalla = true;
            //INICIAR EL TIMER DE PREGUNTA//
            if (esPrimeraPregunta)
                timerBatallaPrimeraPregunta.Change(0, 1000 * 1);
            else
                timerBatallaSegundaPregunta.Change(0, 1000 * 1);
        }

        public void CargarNuevaPregunta()
        {
            var pregunta = GameLogic.HelperLogic.ObtenerPreguntaAleatoria(App.context);
            var opciones = GameLogic.HelperLogic.ObtenerOpciones(App.context, pregunta.PreguntaId);
            PreguntaTXT.Text = pregunta.TextoPregunta;
            for (int i = 0; i < opciones.Count; i++)
            {
                ((TextBlock)FindName("Opcion" + (i + 1) + "TXT")).Text = opciones[i].TextoOpcion;
            }
            objJuego.Pregunta = pregunta;
            objJuego.Opciones = opciones;
        }

        public void ModificarComboAtaque(int direccion)
        {
            int comboActual = int.Parse(txtComboAtk.Text);
            if (direccion == Constantes.Controles.IZQUIERDA)
            {
                if (comboActual - 1 <= 0)
                {
                    return;
                }
                txtComboAtk.Text = (comboActual - 1).ToString();
                RightArrowBatallaGrid.Visibility = Visibility.Visible;
                if (comboActual - 2 <= 0)
                {
                    LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
                }
            }
            else if (direccion == Constantes.Controles.DERECHA)
            {
                if (comboActual + 1 > objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas || comboActual + 1 >= objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas)
                {
                    return;
                }
                txtComboAtk.Text = (comboActual + 1).ToString();
                LeftArrowBatallaGrid.Visibility = Visibility.Visible;
                if (comboActual + 2 > objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas || comboActual + 2 >= objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas)
                {
                    RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        //Cuando inicia la batalla, se muestra la pregunta
        public void IniciarSegundaPregunta()
        {
            esPrimeraPregunta = false;
            PanelMensajeJugadorAtacante.Visibility = Visibility.Collapsed;

            Opcion1Canvas.Visibility = Visibility.Collapsed;
            Opcion2Canvas.Visibility = Visibility.Collapsed;
            Opcion3Canvas.Visibility = Visibility.Collapsed;
            Opcion4Canvas.Visibility = Visibility.Collapsed;
            Opcion1Jugador1.Visibility = Visibility.Collapsed;
            Opcion2Jugador1.Visibility = Visibility.Collapsed;
            Opcion3Jugador1.Visibility = Visibility.Collapsed;
            Opcion4Jugador1.Visibility = Visibility.Collapsed;
            Opcion1Jugador2.Visibility = Visibility.Collapsed;
            Opcion2Jugador2.Visibility = Visibility.Collapsed;
            Opcion3Jugador2.Visibility = Visibility.Collapsed;
            Opcion4Jugador2.Visibility = Visibility.Collapsed;

            respuestaAtacante = -1;
            CargarNuevaPregunta();
            timerInicioBatalla.Change(0, 1000 * 1);
        }

        private async void MiMetodoReceptorMesaJuegoHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    #region El jugador ha presiona una tecla
                    if (strMensaje.Trim().Contains(Constantes.JugadorPresionaBoton))
                    {
                        #region Se recibe el mensaje (boton presionado)
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
                        #endregion
                        #region DESPLIEGUE
                        if (objJuego.FaseActual == Constantes.FaseJuego.DESPLIEGUE)
                        {
                            //Boton direccional
                            if (botonPresionado >= 0 && botonPresionado <= 3)
                            {
                                MoverTerritorio(botonPresionado);
                            }
                            else if (botonPresionado == Constantes.Controles.EQUIS)
                            {
                                //Verificar que el jugador tiene unidades para desplegar
                                //Esto no pasará, porque cuando se acaben las unidades se pasará a ataque

                                //Verificar que el territorio perteneza al jugador
                                if (!objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                {
                                    return;
                                }

                                //Realizar despliegue
                                objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas += 1;
                                ((TextBlock)this.FindName("UnidadCantidad" + objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio)).Text =
                                    objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
                                objJuego.UnidadesDisponiblesParaDesplegar--;
                                //ActualizarNumeroTerritoriosInfo();
                                txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();

                                //Si el jugador se queda sin unidades para desplegar, cambiar fase y acción
                                if (objJuego.UnidadesDisponiblesParaDesplegar == 0)
                                {
                                    objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    btnFaseDespliegue.IsEnabled = false;
                                    btnFaseAtaque.IsEnabled = true;
                                }
                            }
                            else if (botonPresionado == Constantes.Controles.TRIANGULO)
                            {
                                //El jugador está pidiendo terminar la fase de Despliegue y pasar a Ataque
                                objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                btnFaseDespliegue.IsEnabled = false;
                                btnFaseAtaque.IsEnabled = true;
                            }
                        }
                        #endregion
                        #region ATAQUE
                        else if (objJuego.FaseActual == Constantes.FaseJuego.ATAQUE)
                        {
                            #region Elegir Origen Ataque
                            if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRORIGENATK)
                            {
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 3)
                                {
                                    MoverTerritorio(botonPresionado);
                                }
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    //Verificar que el territorio perteneza al jugador
                                    if (!objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                    {
                                        return;
                                    }

                                    //Verificar que el origen tenga al menos 2 unidades
                                    if (objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas < 2)
                                    {
                                        return;
                                    }

                                    //Marcar territorio como origen ataque
                                    objJuego.TerritorioOrigenAtaque = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];
                                    objJuego.TerritorioDestinoAtaque = null;
                                    TerrSelec.Opacity = 1;

                                    //Cambiar a accion elegir destino ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    objJuego.FaseActual = Constantes.FaseJuego.FORTIFICACION;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENFOR;
                                    Helper.MensajeOk("Solicitaste terminar la fase de Ataque");
                                }
                            }
                            #endregion
                            #region Elegir DestinoAtaque
                            else if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRDESTINOATK)
                            {
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 3)
                                {
                                    MoverTerritorio(botonPresionado);
                                }
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    //Verificar que el territorio no le pertenezca al jugador
                                    if (objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                    {
                                        return;
                                    }

                                    //Verificar que el territorio es colindante con origen ataque
                                    //////////////////////////////////////Por verificar//////////////////////////////////////////////////////////////////////////////

                                    //Se marca como territorio destino ataque
                                    objJuego.TerritorioDestinoAtaque = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];
                                    objJuego.IpJugadorDefiende = objJuego.TerritorioDestinoAtaque.IpJugadorPropietario;

                                    //Comienza la batalla
                                    objJuego.AccionActual = Constantes.AccionJuego.CONFIRMARATAQUE;
                                    InicializarGridBatallaAtaque();
                                    BatallaGrid.Visibility = Visibility.Visible;
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador cancela la seleccion se destino de ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    ((Path)FindName(objJuego.TerritorioOrigenAtaque.NombreTerritorio)).Opacity = 0;
                                    objJuego.TerritorioOrigenAtaque = null;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    objJuego.FaseActual = Constantes.FaseJuego.FORTIFICACION;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENFOR;
                                    Helper.MensajeOk("Solicitaste terminar la fase de Ataque");
                                }
                            }
                            #endregion
                            #region Elegir Combo de Ataque e Iniciar la Batalla
                            else if (objJuego.AccionActual == Constantes.AccionJuego.CONFIRMARATAQUE)
                            {
                                //Boton direccional
                                if (botonPresionado == Constantes.Controles.IZQUIERDA || botonPresionado == Constantes.Controles.DERECHA)
                                {
                                    ModificarComboAtaque(botonPresionado);
                                }
                                //Se confirma el ataque
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    await App.objSDK.UnicastPing(new HostName(objJuego.IpJugadorDefiende),
                                        Constantes.MesaConumicaHABILITARControles);
                                    IniciarBatalla();
                                    objJuego.AccionActual = Constantes.AccionJuego.BATALLA_PRIMERA_PREGUNTA;
                                }
                                //Se cancela el ataque
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador cancela el inicio del ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioDestinoAtaque = null;
                                    BatallaGrid.Visibility = Visibility.Collapsed;
                                    objJuego.IpJugadorDefiende = "";
                                }
                            }
                            #endregion
                            #region Batalla - Primera Pregunta
                            else if (objJuego.AccionActual == Constantes.AccionJuego.BATALLA_PRIMERA_PREGUNTA)
                            {
                                if (inicioBatalla)
                                {
                                    //El jugador aún no responde la pregunta
                                    if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        if (respuestaAtacante != -1)
                                            return;
                                        else
                                        if (respuestaDefensor != -1)
                                            return;
                                    //Guardar la respuesta del jugador
                                    if (botonPresionado == Constantes.Controles.TRIANGULO)
                                    {
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        {
                                            respuestaAtacante = 0;
                                            puntosAtacante = contadorSegDuracionTimer.Value;
                                            Opcion1Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion1Jugador1.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            respuestaDefensor = 0;
                                            puntosDefensor = contadorSegDuracionTimer.Value;
                                            Opcion1Jugador2.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion1Jugador2.Visibility = Visibility.Visible;
                                        }
                                    }
                                    if (botonPresionado == Constantes.Controles.CUADRADO)
                                    {
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        {
                                            respuestaAtacante = 1;
                                            puntosAtacante = contadorSegDuracionTimer.Value;
                                            Opcion2Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion2Jugador1.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            respuestaDefensor = 1;
                                            puntosDefensor = contadorSegDuracionTimer.Value;
                                            Opcion2Jugador2.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion2Jugador2.Visibility = Visibility.Visible;
                                        }
                                    }
                                    if (botonPresionado == Constantes.Controles.CIRCULO)
                                    {
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        {
                                            respuestaAtacante = 2;
                                            puntosAtacante = contadorSegDuracionTimer.Value;
                                            Opcion3Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion3Jugador1.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            respuestaDefensor = 2;
                                            puntosDefensor = contadorSegDuracionTimer.Value;
                                            Opcion3Jugador2.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion3Jugador2.Visibility = Visibility.Visible;
                                        }
                                    }
                                    if (botonPresionado == Constantes.Controles.EQUIS)
                                    {
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        {
                                            respuestaAtacante = 3;
                                            puntosAtacante = contadorSegDuracionTimer.Value;
                                            Opcion4Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion4Jugador1.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            respuestaDefensor = 3;
                                            puntosDefensor = contadorSegDuracionTimer.Value;
                                            Opcion4Jugador2.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                            Opcion4Jugador2.Visibility = Visibility.Visible;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region Batalla - Confirmar Inicio Segunda Pregunta
                            else if (objJuego.AccionActual == Constantes.AccionJuego.CONFIRMAR_INICIO_SEGUNDA_PREGUNTA)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    IniciarSegundaPregunta();
                                    objJuego.AccionActual = Constantes.AccionJuego.BATALLA_SEGUNDA_PREGUNTA;
                                }
                            }
                            #endregion
                            #region Batalla - Segunda Pregunta
                            else if (objJuego.AccionActual == Constantes.AccionJuego.BATALLA_SEGUNDA_PREGUNTA)
                            {
                                if (inicioBatalla)
                                {
                                    //El jugador aún no responde la pregunta
                                    if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        if (respuestaAtacante != -1)
                                            return;
                                    //Guardar la respuesta del jugador
                                    if (botonPresionado == Constantes.Controles.TRIANGULO)
                                    {
                                        respuestaAtacante = 0;
                                        puntosAtacante = contadorSegDuracionTimer.Value;
                                        Opcion1Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        Opcion1Jugador1.Visibility = Visibility.Visible;
                                    }
                                    if (botonPresionado == Constantes.Controles.CUADRADO)
                                    {
                                        respuestaAtacante = 1;
                                        puntosAtacante = contadorSegDuracionTimer.Value;
                                        Opcion2Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        Opcion2Jugador1.Visibility = Visibility.Visible;
                                    }
                                    if (botonPresionado == Constantes.Controles.CIRCULO)
                                    {
                                        respuestaAtacante = 2;
                                        puntosAtacante = contadorSegDuracionTimer.Value;
                                        Opcion3Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        Opcion3Jugador1.Visibility = Visibility.Visible;
                                    }
                                    if (botonPresionado == Constantes.Controles.EQUIS)
                                    {
                                        respuestaAtacante = 3;
                                        puntosAtacante = contadorSegDuracionTimer.Value;
                                        Opcion4Jugador1.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        Opcion4Jugador1.Visibility = Visibility.Visible;
                                    }
                                }
                            }
                            #endregion
                            #region Confirmar Inicio Mover Tropas
                            else if (objJuego.AccionActual == Constantes.AccionJuego.CONFIRMAR_INICIO_MOVERTROPAS)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    IniciarMoverTropas();
                                    objJuego.AccionActual = Constantes.AccionJuego.MOVERTROPAS;
                                }
                            }
                            #endregion
                            #region Mover Tropas
                            else if (objJuego.AccionActual == Constantes.AccionJuego.MOVERTROPAS)
                            {
                            }
                            #endregion
                            #region Decidir se se continuará con el Ataque
                            else if (objJuego.AccionActual == Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    objJuego.AccionActual = Constantes.AccionJuego.CONFIRMARATAQUE;
                                    InicializarGridBatallaAtaque();
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador decide finalizar el ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioDestinoAtaque = null;
                                    BatallaGrid.Visibility = Visibility.Collapsed;
                                    objJuego.IpJugadorDefiende = "";
                                }
                            }
                            #endregion
                            #region Terminar el Ataque
                            else if (objJuego.AccionActual == Constantes.AccionJuego.TERMINAR_ATAQUE)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    //Se termina el ataque ya que el Atacante solo tiene 1 unidad en el territorio
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioDestinoAtaque = null;
                                    BatallaGrid.Visibility = Visibility.Collapsed;
                                    objJuego.IpJugadorDefiende = "";
                                }
                            }
                            #endregion
                        }
                        #endregion
                        #region FORTIFICACION
                        else if (objJuego.FaseActual == Constantes.FaseJuego.FORTIFICACION)
                        {
                            #region Fortificar - Elegir el Origen de la fortificacion
                            if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRORIGENFOR)
                            {
                            }
                            #endregion
                            #region Fortificar - Elegir el Destino de la fortificacion
                            else if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRDESTINOFOR)
                            {
                            }
                            #endregion
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



        #region BOTONES DEL TABLERO
        private void BtnArr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ARRIBA);
        }

        private void BtnIzq_Tapped(object sender, TappedRoutedEventArgs e)
        {
           MoverTerritorio(Constantes.Controles.IZQUIERDA);
        }

        private void BtnDer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.DERECHA);
        }

        private void BtnAba_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ABAJO);
        }

        private void BtnArrIzq_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ARRIBAIZQUIERDA);
        }

        private void BtnArrDer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ARRIBADERECHA);
        }

        private void BtnAbaIzq_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ABAJOIZQUIERDA);
        }

        private void BtnAbaDer_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MoverTerritorio(Constantes.Controles.ABAJODERECHA);
        }

        private void XButtonGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {

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
                ActualizarNumeroTerritoriosInfo();
            }
        }
        #endregion

        public void MostrarBlanco()
        {
            Blanco.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 0];
            XButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 1];
            OButtonGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 2];
            DownArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 3];
            UpArrowGrid.Visibility = BlancoGridVisibilidad[objJuego.FaseActual, 4];
        }

        
    }
}
