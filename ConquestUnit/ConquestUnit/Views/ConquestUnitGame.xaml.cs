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
        private Timer timerDuracionBatalla;
        private int? contadorSegDuracionTimer;
        private int? contadorMilDuracionTimer;
        //private DateTime horaInicioTimer;
        private int? respuestaAtacante;
        private int? respuestaDefensor;

        private float puntosAtacante;
        private float puntosDefensor;

        public ConquestUnitGame()
        {
            this.InitializeComponent();

            //DispatcherTimer timer = new DispatcherTimer();
            //timer.Interval = TimeSpan.FromSeconds(1);
            //timer.Tick += timer_Tick;
            //timer.Start();
            //timer.Tick 
            timerInicioBatalla = new Timer(timerInicioBatallaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerDuracionBatalla = new Timer(timerDuracionBatallaCallback, null, Timeout.Infinite, Timeout.Infinite);

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
            DibujarJugadores();
            DibujarUnidadesTerritorioEnElMapa();
            ActualizarNumeroTerritoriosInfo();
            ActualizarNumeroContinentesInfo();
            ActualizarNumeroUnidadesParaDespliegue();

            objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
            objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;
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

        public async void InicializarGridBatallaAtaque()
        {
            //Area combo de Ataque
            txtComboAtk.Text = "1";
            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            if (objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas > 1)
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
            PreguntaGrid.Visibility = Visibility.Collapsed;
            txtTimerOpciones.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Visibility = Visibility.Collapsed;
            Opcion2Canvas.Visibility = Visibility.Collapsed;
            Opcion3Canvas.Visibility = Visibility.Collapsed;
            Opcion4Canvas.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion2Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion3Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion4Canvas.Background = new SolidColorBrush(Colors.Transparent);
            Opcion1Correcto.Visibility = Visibility.Collapsed;
            Opcion2Correcto.Visibility = Visibility.Collapsed;
            Opcion3Correcto.Visibility = Visibility.Collapsed;
            Opcion4Correcto.Visibility = Visibility.Collapsed;
            Opcion1Incorrecto.Visibility = Visibility.Collapsed;
            Opcion2Incorrecto.Visibility = Visibility.Collapsed;
            Opcion3Incorrecto.Visibility = Visibility.Collapsed;
            Opcion4Incorrecto.Visibility = Visibility.Collapsed;
            //Area Atacante
            var atacante = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorTurnoActual).First();
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

        public void IniciarBatalla()
        {
            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
            XButtonGrid_Copy.Visibility = Visibility.Collapsed;
            AtacarBatallaTXT.Visibility = Visibility.Collapsed;

            PreguntaGrid.Visibility = Visibility.Visible;
            txtTimerOpciones.Visibility = Visibility.Visible;
            puntosAtacante = 0;
            puntosDefensor = 0;
            contadorInicioTimer = null;
            contadorSegDuracionTimer = null;
            contadorMilDuracionTimer = null;
            inicioBatalla = false;
            respuestaAtacante = null;
            respuestaDefensor = null;
            CargarNuevaPregunta();
            timerInicioBatalla.Change(0, 1000 * 1);
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

        public async void timerDuracionBatallaCallback(object sender)
        {
            if (contadorSegDuracionTimer == null)
            {
                contadorSegDuracionTimer = 10;
                contadorMilDuracionTimer = 0;
            }
            if (respuestaAtacante != null && puntosAtacante!=0)
            {
                puntosAtacante = float.Parse(contadorSegDuracionTimer.ToString() + "." + contadorMilDuracionTimer.ToString());
            }
            if (respuestaDefensor != null && puntosDefensor != 0)
            {
                puntosDefensor = float.Parse(contadorSegDuracionTimer.ToString() + "." + contadorMilDuracionTimer.ToString());
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                // do some work on UI here;
                txtTimerOpciones.Text = contadorSegDuracionTimer.ToString() + "." + contadorMilDuracionTimer.ToString();
                if (contadorMilDuracionTimer == 0)
                {
                    contadorMilDuracionTimer = 9;
                    contadorSegDuracionTimer--;
                }
                else
                {
                    contadorMilDuracionTimer--;
                }

                if (contadorSegDuracionTimer < 0)
                {
                    timerDuracionBatalla.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorSegDuracionTimer = null;
                    contadorMilDuracionTimer = null;
                    //Mostrar puntuaciones de los jugadores
                    //Verificar quien tuvo la respuesta correcta y acreditar los puntos
                    InvasorPuntosAtaque1TXT.Text = puntosAtacante.ToString();
                    InvasorPuntosDefensaTXT.Text = puntosDefensor.ToString();

                    for (int i = 0; i < objJuego.Opciones.Count; i++)
                    {
                        if (objJuego.Opciones[i].EsRespuesta)
                        {
                            ((Image)FindName("Opcion" + (i + 1) + "Correcto")).Visibility = Visibility.Visible;
                            break;
                        }
                    }
                    Helper.MensajeOk("Acabo la batalla.Puntos sin validaar respuesta correcta.");

                }
            });
        }

        public void MostrarOpciones()
        {
            //txtTimerOpciones.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Visibility = Visibility.Visible;
            Opcion2Canvas.Visibility = Visibility.Visible;
            Opcion3Canvas.Visibility = Visibility.Visible;
            Opcion4Canvas.Visibility = Visibility.Visible;
            inicioBatalla = true;
            //INICIAR EL TIMER DE PREGUNTA//
            timerDuracionBatalla.Change(0, 100);
            //Helper.MensajeOk("ESTAMOS EN BATALLA!!!");
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
                if (comboActual + 1 > objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas)
                {
                    return;
                }
                txtComboAtk.Text = (comboActual + 1).ToString();
                LeftArrowBatallaGrid.Visibility = Visibility.Visible;
                if (comboActual + 2 > objJuego.TerritorioDestinoAtaque.NUnidadesDeplegadas)
                {
                    RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
                }
            }
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
                                ActualizarNumeroTerritoriosInfo();
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

                                    //Verificar que el territorio tiene menos o igual unidades que el origen de ataque
                                    if (objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas > objJuego.TerritorioOrigenAtaque.NUnidadesDeplegadas)
                                    {
                                        return;
                                    }

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
                                    objJuego.AccionActual = Constantes.AccionJuego.BATALLA;
                                }
                                //Se cancela el ataque
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador cancela la seleccion se destino de ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioDestinoAtaque = null;
                                    BatallaGrid.Visibility = Visibility.Collapsed;
                                    objJuego.IpJugadorDefiende = "";
                                }
                            }
                            #endregion
                            #region BATALLAAAAAAAAAA
                            else if (objJuego.AccionActual == Constantes.AccionJuego.BATALLA)
                            {
                                if (inicioBatalla)
                                {
                                    //El jugador aún no responde la pregunta
                                    if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                        if (respuestaAtacante != null)
                                            return;
                                    else
                                        if (respuestaDefensor != null)
                                            return;
                                    //Guardar la respuesta del jugador
                                    if (botonPresionado == Constantes.Controles.TRIANGULO)
                                    {
                                        Opcion1Canvas.Background = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                            respuestaAtacante = 0;
                                        else
                                            respuestaDefensor = 0;
                                    }
                                    if (botonPresionado == Constantes.Controles.CUADRADO)
                                    {
                                        Opcion2Canvas.Background = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                            respuestaAtacante = 1;
                                        else
                                            respuestaDefensor = 1;
                                    }
                                    if (botonPresionado == Constantes.Controles.CIRCULO)
                                    {
                                        Opcion3Canvas.Background = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                            respuestaAtacante = 2;
                                        else
                                            respuestaDefensor = 2;
                                    }
                                    if (botonPresionado == Constantes.Controles.EQUIS)
                                    {
                                        Opcion4Canvas.Background = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados.Where(x => x.Ip == mensaje[1]).First().Color);
                                        if (mensaje[1] == objJuego.IpJugadorTurnoActual)
                                            respuestaAtacante = 3;
                                        else
                                            respuestaDefensor = 3;
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                        #region FORTIFICACION
                        else if (objJuego.FaseActual == Constantes.FaseJuego.FORTIFICACION)
                        {

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
