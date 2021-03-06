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

        private Timer timerInicioBatalla;
        private int? contadorInicioTimer;
        private bool inicioBatalla;
        private bool esPrimeraPregunta;
        private Timer timerBatallaPrimeraPregunta;
        private int? contadorSegDuracionTimer;
        private Timer timerBatallaSegundaPregunta;
        private int? contadorTimerSiguienteTurno;
        private Timer timerSiguienteTurno;
        private int? contadorTimerDerrotaJugador;
        private Timer timerDerrotaJugador;
        private int respuestaAtacante;
        private int respuestaDefensor;
        private Timer timerMantenerConexion;

        private int auxAccionJuegoAnterior;

        private int puntosAtacante;
        private int puntosDefensor;

        public ConquestUnitGame()
        {
            this.InitializeComponent();

            timerInicioBatalla = new Timer(timerInicioBatallaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerBatallaPrimeraPregunta = new Timer(timerBatallaPrimeraPreguntaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerBatallaSegundaPregunta = new Timer(timerBatallaSegundaPreguntaCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerSiguienteTurno = new Timer(timerSiguienteTurnoCallback, null, Timeout.Infinite, Timeout.Infinite);
            timerDerrotaJugador = new Timer(timerDerrotaJugadorCallback, null, Timeout.Infinite, Timeout.Infinite);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            DisplayInformation.AutoRotationPreferences = DisplayOrientations.Landscape;
            ConquestUnitMusic.SetPlaybackSource(Windows.Media.Core.MediaSource.CreateFromUri(new Uri(Constantes.Media.Music.MusicaConquestUnit)));
            ConquestUnitMusic.IsLooping = true;
            ConquestUnitMusic.Volume = 1;
            ConquestUnitMusic.Play();

            if (e.Parameter != null)
            {
                objJuego = (Juego)e.Parameter;
            }
            else
            {
                Helper.MensajeOk("No se pudo iniciar el juego.");
            }

            IniciarSDK();
            Inicializar();

            timerMantenerConexion = new Timer(timerMantenerConexionCallback, null, TimeSpan.FromSeconds(3), TimeSpan.FromSeconds(Constantes.KeepAlive));
        }

        #region Revision de conexion
        private async void timerMantenerConexionCallback(object state)
        {
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                IniciarSDK();
            });
        }
        #endregion

        public void Inicializar()
        {
            BatallaGrid.Visibility = Visibility.Collapsed;
            MoverTropasGrid.Visibility = Visibility.Collapsed;
            //Inicializar Mapa
            if (objJuego.TipoMapa == Constantes.MAPA.CHINA)
            {
                Mapa_China.Visibility = Visibility.Visible;
                Mapa_Roma.Visibility = Visibility.Collapsed;
                Mapa_NorteAmerica.Visibility = Visibility.Collapsed;
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
                TerrSelec = Huijiang;
            }
            else if (objJuego.TipoMapa == Constantes.MAPA.ROMA)
            {
                Mapa_China.Visibility = Visibility.Collapsed;
                Mapa_Roma.Visibility = Visibility.Visible;
                Mapa_NorteAmerica.Visibility = Visibility.Collapsed;
                Territorio = new Windows.UI.Xaml.Shapes.Path[24, 8]
                {
                {null,Augusta,Augusta,Augusta,Tingitana,null,null,null},//Lusitania
                {Aquitania,Alpina,Sardinia,Tingitana,Tingitana,Lusitania,Lusitania,Lusitania },//Augusta
                {Flavia,Frisia,Alpina,Alpina,Augusta,Augusta,null,Flavia},//Aquitania
                {Frisia,Roma,Roma,Sardinia,Augusta,Augusta,Aquitania,Aquitania},//Alpina
                {null,null,Pannonia,Pannonia,Alpina,Aquitania,Aquitania,Flavia},//Frisia
                {null,null,Frisia,Frisia,Aquitania,null,null,null},//Flavia

                {Lusitania,Augusta,Carthago,Carthago,null,null,null,Lusitania },//Tingitana
                {Sardinia,Roma,Libya,Libya,null,Tingitana,Tingitana,null },//Carthago
                {null,Cyrene,Cyrene,Cyrene,null,Carthago,Carthago,Carthago},//Libya
                {null,Arcadia,Arcadia,Arcadia,null,Libya,Libya,Libya},//Cyrene
                {null,Salutaris,Salutaris,Thebaida,Thebaida,Cyrene,Cyrene,Cyrene},//Arcadia
                {Arcadia,Arcadia,null,null,null,null,null,Arcadia },//Thebaida

                {Moesia,Capadocia,Siria,Siria,null,Peloponense,Peloponense,Moesia },//Lydia
                {null,Armenia,Armenia,Siria,Lydia,Lydia,Lydia,null },//Capadocia
                {null,null,null,Mesopotania,Mesopotania,Capadocia,Capadocia,null },//Armenia
                {Armenia,null,null,null,Siria,Siria,Siria,Armenia },//Mesopotania
                {Capadocia,Mesopotania,null,null,Salutaris,null,Lydia,Lydia },//Siria
                {Siria,Siria,null,null,null,Arcadia,Arcadia,null },//Salutaris

                {Roma,Roma,Roma,Carthago,Carthago,Carthago,Augusta,Alpina},//Sardinia
                {Pannonia,Pannonia,Dalmatia,Dalmatia,Carthago,Carthago,Sardinia,Alpina },//Roma
                {Pannonia,Moesia,Moesia,Peloponense,null,Roma,Roma,Pannonia },//Dalmatia
                {null,null,Lydia,Lydia,Peloponense,Dalmatia,Pannonia,Pannonia },//Moesia
                {null,null,Moesia,Dalmatia,Roma,Roma,Frisia,Frisia },//Pannonia
                {Moesia,Moesia,Lydia,Lydia,null,null,Dalmatia,Dalmatia }//Peloponense
                };
                TerrSelec = Lusitania;
            }
            else if (objJuego.TipoMapa == Constantes.MAPA.NORTEAMERICA)
            {
                Mapa_China.Visibility = Visibility.Collapsed;
                Mapa_Roma.Visibility = Visibility.Collapsed;
                Mapa_NorteAmerica.Visibility = Visibility.Visible;
                Territorio = new Windows.UI.Xaml.Shapes.Path[24, 8]
                {
                {PendArtica,PendArtica,Yukon,Columbia,Columbia,null,null,null},//Doyon
                {null,null,Yukon,Yukon,Doyon,Doyon,null,null },//PendArtica
                {PendArtica,TerritNores,TerritNores,Columbia,Columbia,Columbia,Doyon,PendArtica},//Yukon
                {Numavut,Numavut,Numavut,Manitoba,Alberta,Columbia,Yukon,Yukon},//TerritNores
                {Yukon,TerritNores,Alberta,Montana,California,null,null,Doyon},//Columbia
                {TerritNores,TerritNores,Manitoba,Montana,Montana,Montana,Columbia,Columbia},//Alberta

                {Columbia,Montana,Utah,Sonora,Sonora,null,null,null },//California
                {Montana,Dakota,Texas,Sonora,Sonora,Sonora,California,Montana },//Utah
                {Utah,Texas,Monterrey,Monterrey,Guerrero,null,null,California},//Sonora
                {Texas,Texas,null,Guerrero,Guerrero,Guerrero,Sonora,Sonora},//Monterrey
                {Monterrey,null,Yucatan,null,null,null,null,Sonora},//Guerrero
                {null,null,null,null,null,null,Guerrero,Guerrero },//Yucatan

                {Alberta,Manitoba,Dakota,Utah,Utah,California,California,Columbia },//Montana
                {Manitoba,Ontario,Ohio,Texas,Texas,Utah,Montana,Manitoba },//Dakota
                {Dakota,Ohio,Kentucky,null,null,Monterrey,Sonora,Utah },//Texas
                {Ontario,Ontario,NuevaYork,Kentucky,Kentucky,Texas,Dakota,Dakota },//Ohio
                {Ohio,NuevaYork,null,null,null,Texas,Texas,Ohio },//Kentucky
                {Quebec,Quebec,null,null,Kentucky,Kentucky,Ohio,Ontario },//NuevaYork

                {null,null,null,Quebec,Quebec,Quebec,null,null},//Labrador
                {Labrador,Labrador,null,NuevaYork,NuevaYork,Ontario,Ontario,null },//Quebec
                {null,null,Quebec,NuevaYork,Ohio,Dakota,Manitoba,Manitoba },//Ontario
                {Numavut,null,Ontario,Dakota,Dakota,Montana,Alberta,TerritNores },//Manitoba
                {Victoria,null,null,null,Manitoba,TerritNores,TerritNores,Victoria },//Numavut
                {null,null,Numavut,Numavut,Numavut,null,null,null }//Victoria
                };
                TerrSelec = Dakota;
            }
            //Dibujar unidades y Cantidad de unidades
            DibujarJugadores();
            IniciarSiguienteTurno();
            DibujarUnidadesTerritorioEnElMapa();
            ActualizarNumeroTerritoriosInfo();
            ActualizarNumeroContinentesInfo();
            ActualizarNumeroUnidadesInfo();

            btnFaseDespliegue.IsEnabled = true;
            btnFaseAtaque.IsEnabled = false;
            btnFaseFortificacion.IsEnabled = false;
        }

        public void ActualizarDespuesDeBatalla()
        {
            //Mapa
            var territorio = objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId];
            Uri uri = new Uri(territorio.ImagenUnidades);
            BitmapImage imagenUnidad = new BitmapImage(uri);
            ((Image)this.FindName("Unidad" + territorio.NombreTerritorio)).Source = imagenUnidad;
            ((Ellipse)this.FindName("UnidadElipse" + territorio.NombreTerritorio)).Fill = Convertidor.GetSolidColorBrush(territorio.ColorUnidades);
            ((TextBlock)this.FindName("UnidadCantidad" + territorio.NombreTerritorio)).Text = territorio.NUnidadesDeplegadas.ToString();

            territorio = objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId];
            uri = new Uri(territorio.ImagenUnidades);
            imagenUnidad = new BitmapImage(uri);
            ((Image)this.FindName("Unidad" + territorio.NombreTerritorio)).Source = imagenUnidad;
            ((Ellipse)this.FindName("UnidadElipse" + territorio.NombreTerritorio)).Fill = Convertidor.GetSolidColorBrush(territorio.ColorUnidades);
            ((TextBlock)this.FindName("UnidadCantidad" + territorio.NombreTerritorio)).Text = territorio.NUnidadesDeplegadas.ToString();

            ActualizarCuadroInfoTerritorio();

            ActualizarNumeroTerritoriosInfo();
            ActualizarNumeroContinentesInfo();
            ActualizarNumeroUnidadesInfo();
        }

        public void ActualizarCuadroInfoTerritorio()
        {
            MapaTerritorioSeleccionadoTexto.Text = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio;
            MapaTerritorioSeleccionadoUnidades.Text = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
            MapaTerritorioSeleccionadoPropietario.Text = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreJugadorPropietario;
            MapaTerritorioSeleccionadoColor.Fill = Convertidor.GetSolidColorBrush(objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].ColorUnidades);
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
            //Despintar el territorio anterior
            if (objJuego.TerritorioAtaqueOrigen != null || objJuego.TerritorioFortificacionOrigen != null)
            {
                int indiceSeleccionado = 0;
                if (objJuego.TerritorioAtaqueOrigen != null)
                {
                    indiceSeleccionado = objJuego.TerritorioAtaqueOrigen.TerritorioId;
                }
                else
                {
                    indiceSeleccionado = objJuego.TerritorioFortificacionOrigen.TerritorioId;
                }
                if (TerrSelec.Name != objJuego.Territorios[indiceSeleccionado].NombreTerritorio)
                {
                    TerrSelec.Fill = null;
                }
            }
            else
            {
                TerrSelec.Fill = null;
            }

            Grid GrdTerritorioTmp;
            if (BlancoGrid.Parent != null)
            {
                GrdTerritorioTmp = (Grid)BlancoGrid.Parent;
                GrdTerritorioTmp.Children.Remove(BlancoGrid);
            }
            TerrSelec = territorio;
            GrdTerritorioTmp = (Grid)this.FindName("Grd" + TerrSelec.Name);

            GrdTerritorioTmp.Children.Add(BlancoGrid);

            //Pintar el territorio anterior
            if (objJuego.TerritorioAtaqueOrigen != null || objJuego.TerritorioFortificacionOrigen != null)
            {
                int indiceSeleccionado = 0;
                if (objJuego.TerritorioAtaqueOrigen != null)
                {
                    indiceSeleccionado = objJuego.TerritorioAtaqueOrigen.TerritorioId;
                }
                else
                {
                    indiceSeleccionado = objJuego.TerritorioFortificacionOrigen.TerritorioId;
                }
                if (TerrSelec.Name != objJuego.Territorios[indiceSeleccionado].NombreTerritorio)
                {
                    TerrSelec.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadorTurnoActual().ColorPastel);
                }
            }
            else
            {
                TerrSelec.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadorTurnoActual().ColorPastel);
            }
            ActualizarCuadroInfoTerritorio();
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

        public void DibujarJugadorEnTurno()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                if (objJuego.IpJugadorTurnoActual.Equals(objJuego.JugadoresConectados[i].Ip))
                {
                    ((SolidColorBrush)FindName("UnidadGrid" + (i + 1) + "Fondo")).Opacity = 0.8;
                    JuegoJugadorTurnoActualNombre.Text = objJuego.JugadoresConectados[i].Nombre;
                    JuegoJugadorTurnoActualColor.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadoresConectados[i].Color);
                }
                else
                {
                    ((SolidColorBrush)FindName("UnidadGrid" + (i + 1) + "Fondo")).Opacity = 0.5;
                }
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

        public void ActualizarNumeroUnidadesInfo()
        {
            for (int i = 0; i < objJuego.JugadoresConectados.Count; i++)
            {
                ((TextBlock)this.FindName("Jugador" + (i + 1) + "Unidades")).Text =
                    GameLogic.HelperLogic.NroUnidadesJugador(objJuego, objJuego.JugadoresConectados[i].Ip).ToString();
            }
        }

        public void ActualizarNumeroUnidadesJugadorEnTurnoInfo()
        {
            ((TextBlock)this.FindName("Jugador" + (objJuego.TurnoActual + 1) + "Unidades")).Text =
                    GameLogic.HelperLogic.NroUnidadesJugador(objJuego, objJuego.JugadoresConectados[objJuego.TurnoActual].Ip).ToString();
        }

        public void ActualizarNumeroUnidadesParaDespliegue()
        {
            GameLogic.DespliegueLogic.UnidadesDisponiblesDesplegarJugadorTurnoActual(objJuego);
            PanelUnidadesParaDesplegar.Visibility = Visibility.Visible;
            txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();
        }

        public async void InicializarGridBatallaAtaque()
        {
            //Area combo de Ataque
            txtComboAtk.Text = "1";
            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            if (objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas > 1 && objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas != 2)
            {
                RightArrowBatallaGrid.Visibility = Visibility.Visible;
            }
            else
            {
                RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
            }
            XButtonGrid_Copy.Visibility = Visibility.Visible;
            AtacarBatallaTXT.Visibility = Visibility.Visible;
            AtacarBatallaTXTImg.Visibility = Visibility.Visible;
            //Area Pregunta
            CabeceraGrid.Visibility = Visibility.Visible;
            PreguntaGrid.Visibility = Visibility.Collapsed;
            MoverTropasGrid.Visibility = Visibility.Collapsed;
            txtTimerOpciones.Visibility = Visibility.Collapsed;
            Opcion1Canvas.Visibility = Visibility.Collapsed;
            Opcion2Canvas.Visibility = Visibility.Collapsed;
            Opcion3Canvas.Visibility = Visibility.Collapsed;
            Opcion4Canvas.Visibility = Visibility.Collapsed;
            Opcion1CanvasColor.Color = Colors.Black;
            Opcion2CanvasColor.Color = Colors.Black;
            Opcion3CanvasColor.Color = Colors.Black;
            Opcion4CanvasColor.Color = Colors.Black;
            Opcion1Jugador1.Visibility = Visibility.Collapsed;
            Opcion2Jugador1.Visibility = Visibility.Collapsed;
            Opcion3Jugador1.Visibility = Visibility.Collapsed;
            Opcion4Jugador1.Visibility = Visibility.Collapsed;
            Opcion1Jugador2.Visibility = Visibility.Collapsed;
            Opcion2Jugador2.Visibility = Visibility.Collapsed;
            Opcion3Jugador2.Visibility = Visibility.Collapsed;
            Opcion4Jugador2.Visibility = Visibility.Collapsed;
            //Area Atacante
            var atacante = objJuego.JugadorTurnoActual();
            PanelMensajeResultadoBatalla.Visibility = Visibility.Collapsed;
            AtacanteTerritorioNombreTXT.Text = objJuego.TerritorioAtaqueOrigen.NombreTerritorio;
            AtacanteNombreTXT.Text = atacante.Nombre;
            InvasorFondo.Fill = Convertidor.GetSolidColorBrush(atacante.Color);
            if (atacante.Imagen != null)
            {
                BitmapImage bimgBitmapImage = new BitmapImage();
                IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(atacante.Imagen);
                bimgBitmapImage.SetSource(fileStream);
                InvasorFoto.Source = bimgBitmapImage;
            }
            NTropasInvasorTXT.Text = objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas.ToString();
            InvasorPuntosAtaqueTXT.Text = "0";
            //Area Defensor
            var defensor = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorDefiende).First();
            DefensorTerritorioNombreTXT.Text = objJuego.TerritorioAtaqueDestino.NombreTerritorio;
            DefensorNombreTXT.Text = defensor.Nombre;
            DefensorFondo.Fill = Convertidor.GetSolidColorBrush(defensor.Color);
            if (defensor.Imagen != null)
            {
                BitmapImage bimgBitmapImage = new BitmapImage();
                IRandomAccessStream fileStream = await Convertidor.ConvertImageToStream(defensor.Imagen);
                bimgBitmapImage.SetSource(fileStream);
                DefensorFoto.Source = bimgBitmapImage;
            }
            NTropasDefensorTXT.Text = objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas.ToString();
            InvasorPuntosDefensaTXT.Text = "0";
        }

        public void IniciarBatalla()
        {
            esPrimeraPregunta = true;

            LeftArrowBatallaGrid.Visibility = Visibility.Collapsed;
            RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
            XButtonGrid_Copy.Visibility = Visibility.Collapsed;
            AtacarBatallaTXT.Visibility = Visibility.Collapsed;
            AtacarBatallaTXTImg.Visibility = Visibility.Collapsed;

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

        public void IniciarMoverTropas()
        {
            PanelMensajeResultadoBatalla.Visibility = Visibility.Collapsed;
            //Mostrar grilla de mover tropas!!!!!!!!!!!!!!!!!!!!!!!
            CabeceraGrid.Visibility = Visibility.Collapsed;
            PreguntaGrid.Visibility = Visibility.Collapsed;
            MoverTropasGrid.Visibility = Visibility.Visible;
            TituloMoverTropas.Text = Constantes.MOVER_TROPAS_MOVER;
            MoverUnidadesTerritorioOrigen.Text = objJuego.TerritorioAtaqueOrigen.NombreTerritorio;
            MoverUnidadesTerritorioOrigenNUnidades.Text = (objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas - 1).ToString();
            MoverUnidadesTerritorioDestino.Text = objJuego.TerritorioAtaqueDestino.NombreTerritorio;
            MoverUnidadesTerritorioDestinoNUnidades.Text = "1";

            if (objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas < Constantes.UnidadesMinimasParaEvolucionar)
                MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
            else
                MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
            MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));

            MoverUnidadesImgIzquierda.Visibility = Visibility.Collapsed;
            if (objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas <= 2)
            {
                MoverUnidadesImgDerecha.Visibility = Visibility.Collapsed;
            }
            else
            {
                MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
            }
        }

        public void ModificarUnidadesMover(int direccion)
        {
            int unidadesOrigen = int.Parse(MoverUnidadesTerritorioOrigenNUnidades.Text);
            int unidadesDestino = int.Parse(MoverUnidadesTerritorioDestinoNUnidades.Text);

            if (direccion == Constantes.Controles.IZQUIERDA)
            {
                if (unidadesDestino <= 1)
                {
                    return;
                }
                MoverUnidadesTerritorioOrigenNUnidades.Text = (unidadesOrigen + 1).ToString();
                MoverUnidadesTerritorioDestinoNUnidades.Text = (unidadesDestino - 1).ToString();
                unidadesOrigen++;
                unidadesDestino--;
                if (unidadesOrigen == Constantes.UnidadesMinimasParaEvolucionar)
                {
                    MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
                }
                if (unidadesDestino == (Constantes.UnidadesMinimasParaEvolucionar - 1))
                {
                    MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
                }
            }
            else if (direccion == Constantes.Controles.DERECHA)
            {
                if (unidadesOrigen <= 1)
                {
                    return;
                }
                MoverUnidadesTerritorioOrigenNUnidades.Text = (unidadesOrigen - 1).ToString();
                MoverUnidadesTerritorioDestinoNUnidades.Text = (unidadesDestino + 1).ToString();
                unidadesOrigen--;
                unidadesDestino++;
                if (unidadesOrigen == (Constantes.UnidadesMinimasParaEvolucionar - 1))
                {
                    MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
                }
                if (unidadesDestino == Constantes.UnidadesMinimasParaEvolucionar)
                {
                    MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
                }
            }
            if (unidadesOrigen == 1)
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Visible;
                MoverUnidadesImgDerecha.Visibility = Visibility.Collapsed;
            }
            else if (unidadesOrigen == objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas - 1)
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Collapsed;
                MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
            }
            else
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Visible;
                MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
            }
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
                            ((SolidColorBrush)FindName("Opcion" + (i + 1) + "CanvasColor")).Color = Colors.Green;
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
                                ((SolidColorBrush)FindName("Opcion" + (i + 1) + "CanvasColor")).Color = Colors.Red;
                            }
                        }
                    }
                    //Mostrar puntuaciones de los jugadores
                    InvasorPuntosAtaqueTXT.Text = puntosAtacante.ToString();
                    InvasorPuntosDefensaTXT.Text = puntosDefensor.ToString();

                    //Desactivar los controles del defensor
                    EnviarMensajeJugador(objJuego.IpJugadorDefiende, Constantes.MesaConumicaDESHABILITARControles);

                    //Gano el atacante
                    CabeceraGrid.Visibility = Visibility.Collapsed;
                    PanelMensajeResultadoBatalla.Visibility = Visibility.Visible;
                    txtResultadoBatallaPuntosAtacante.Text = puntosAtacante.ToString();
                    txtResultadoBatallaPuntosDefensor.Text = puntosDefensor.ToString();
                    if (puntosAtacante > puntosDefensor)
                    {
                        txtResultadoBatallaComparador.Text = ">";
                        objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        NTropasDefensorTXT.Text = objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas.ToString();
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioAtaqueDestino.NombreTerritorio)).Text = objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas.ToString();
                        var defensor = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.TerritorioAtaqueDestino.IpJugadorPropietario).First();
                        if (objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                            objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = defensor.ImagenUnidadAgrupadora;
                        else
                            objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = defensor.ImagenUnidad;
                        //Mostrar el mensaje de victoria del Atacante
                        txtResultadoBatallaTitulo.Text = Constantes.MensajesResultadoBatalla.Titulo.GANA_EL_ATACANTE;
                        if (objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas == 0)
                        {
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.DESPLEGAR;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Collapsed;
                            objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_INICIO_MOVERTROPAS;
                        }
                        else
                        {
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.CONTINUAR_ATAQUE;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Visible;
                            objJuego.AccionActual = Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE;
                        }
                    }
                    //Gano el defensor
                    else
                    {
                        //Mostrar el mensaje que el atacante puede responder 1 pregunta más
                        txtResultadoBatallaTitulo.Text = Constantes.MensajesResultadoBatalla.Titulo.GANA_EL_DEFENSOR;
                        if (puntosAtacante == puntosDefensor)
                        {
                            txtResultadoBatallaComparador.Text = "=";
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.DEFENSOR_GANA_PRIMERA_PREGUNTA_IGUAL;
                        }
                        else
                        {
                            txtResultadoBatallaComparador.Text = "<";
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.DEFENSOR_GANA_PRIMERA_PREGUNTA_MAYOR;
                        }
                        btnResultadoBatallaCancelar.Visibility = Visibility.Collapsed;
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
                            ((SolidColorBrush)FindName("Opcion" + (i + 1) + "CanvasColor")).Color = Colors.Green;
                            if (respuestaAtacante != i)
                            {
                                puntosAtacante = 0;
                            }
                        }
                        else
                        {
                            if (respuestaAtacante == i)
                            {
                                ((SolidColorBrush)FindName("Opcion" + (i + 1) + "CanvasColor")).Color = Colors.Red;
                            }
                        }
                    }
                    //Mostrar puntuaciones de los jugadores
                    InvasorPuntosAtaqueTXT.Text = puntosAtacante.ToString();

                    //Gano el atacante
                    CabeceraGrid.Visibility = Visibility.Collapsed;
                    PanelMensajeResultadoBatalla.Visibility = Visibility.Visible;
                    txtResultadoBatallaPuntosAtacante.Text = puntosAtacante.ToString();
                    txtResultadoBatallaPuntosDefensor.Text = puntosDefensor.ToString();
                    txtResultadoBatallaTitulo.Text = Constantes.MensajesResultadoBatalla.Titulo.GANA_EL_ATACANTE;
                    if (puntosAtacante > puntosDefensor)
                    {
                        txtResultadoBatallaComparador.Text = ">";
                        objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        NTropasDefensorTXT.Text = objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas.ToString();
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioAtaqueDestino.NombreTerritorio)).Text = objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas.ToString();
                        var defensor = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.TerritorioAtaqueDestino.IpJugadorPropietario).First();
                        if (objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                            objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = defensor.ImagenUnidadAgrupadora;
                        else
                            objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = defensor.ImagenUnidad;
                        //Mostrar el mensaje de victoria del Atacante
                        txtResultadoBatallaTitulo.Text = Constantes.MensajesResultadoBatalla.Titulo.GANA_EL_ATACANTE;
                        if (objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas == 0)
                        {
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.DESPLEGAR_CON_SEGUNDA_PREGUNTA;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Collapsed;
                            objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_INICIO_MOVERTROPAS;
                        }
                        else
                        {
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.CONTINUAR_ATAQUE_CON_SEGUNDA_PREGUNTA;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Visible;
                            objJuego.AccionActual = Constantes.AccionJuego.DECIDIR_CONTINUAR_ATAQUE;
                        }
                    }
                    //Gano el defensor
                    else
                    {
                        objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas -= int.Parse(txtComboAtk.Text);
                        NTropasInvasorTXT.Text = objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas.ToString();
                        ((TextBlock)this.FindName("UnidadCantidad" + objJuego.TerritorioAtaqueOrigen.NombreTerritorio)).Text = objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas.ToString();
                        var atacante = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.TerritorioAtaqueOrigen.IpJugadorPropietario).First();
                        if (objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                            objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].ImagenUnidades = atacante.ImagenUnidadAgrupadora;
                        else
                            objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].ImagenUnidades = atacante.ImagenUnidad;
                        //Mostrar el mensaje de victoria del DEFENSOR
                        txtResultadoBatallaTitulo.Text = Constantes.MensajesResultadoBatalla.Titulo.GANA_EL_DEFENSOR;
                        if (puntosAtacante == puntosDefensor)
                        {
                            txtResultadoBatallaComparador.Text = "=";
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.ATACANTE_PIERDE_SEGUNDA_PREGUNTA_IGUAL;
                        }
                        else
                        {
                            txtResultadoBatallaComparador.Text = "<";
                            txtResultadoBatallaMensaje.Text = Constantes.MensajesResultadoBatalla.Mensaje.ATACANTE_PIERDE_SEGUNDA_PREGUNTA_MAYOR;
                        }

                        if (objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas <= 1)
                        {
                            txtResultadoBatallaMensaje.Text += Constantes.MensajesResultadoBatalla.Mensaje.ATACANTE_PIERDE_SEGUNDA_PREGUNTA_NO_SE_PUEDE_CONTINUAR_ATACANDO;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Collapsed;
                            objJuego.AccionActual = Constantes.AccionJuego.TERMINAR_ATAQUE;
                        }
                        else
                        {
                            txtResultadoBatallaMensaje.Text += Constantes.MensajesResultadoBatalla.Mensaje.ATACANTE_PIERDE_SEGUNDA_PREGUNTA_PREGUNTAR_CONTINUAR_ATAQUE;
                            btnResultadoBatallaCancelar.Visibility = Visibility.Collapsed;
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
            Opcion1CanvasColor.Color = Colors.Black;
            Opcion2CanvasColor.Color = Colors.Black;
            Opcion3CanvasColor.Color = Colors.Black;
            Opcion4CanvasColor.Color = Colors.Black;
            Opcion1Jugador1.Visibility = Visibility.Collapsed;
            Opcion2Jugador1.Visibility = Visibility.Collapsed;
            Opcion3Jugador1.Visibility = Visibility.Collapsed;
            Opcion4Jugador1.Visibility = Visibility.Collapsed;
            Opcion1Jugador2.Visibility = Visibility.Collapsed;
            Opcion2Jugador2.Visibility = Visibility.Collapsed;
            Opcion3Jugador2.Visibility = Visibility.Collapsed;
            Opcion4Jugador2.Visibility = Visibility.Collapsed;
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
                if (comboActual + 1 > objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas || comboActual + 1 >= objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas)
                {
                    return;
                }
                txtComboAtk.Text = (comboActual + 1).ToString();
                LeftArrowBatallaGrid.Visibility = Visibility.Visible;
                if (comboActual + 2 > objJuego.TerritorioAtaqueDestino.NUnidadesDeplegadas || comboActual + 2 >= objJuego.TerritorioAtaqueOrigen.NUnidadesDeplegadas)
                {
                    RightArrowBatallaGrid.Visibility = Visibility.Collapsed;
                }
            }
        }

        public void IniciarSegundaPregunta()
        {
            esPrimeraPregunta = false;
            PanelMensajeResultadoBatalla.Visibility = Visibility.Collapsed;
            CabeceraGrid.Visibility = Visibility.Visible;

            Opcion1Canvas.Visibility = Visibility.Collapsed;
            Opcion2Canvas.Visibility = Visibility.Collapsed;
            Opcion3Canvas.Visibility = Visibility.Collapsed;
            Opcion4Canvas.Visibility = Visibility.Collapsed;
            Opcion1CanvasColor.Color = Colors.Black;
            Opcion2CanvasColor.Color = Colors.Black;
            Opcion3CanvasColor.Color = Colors.Black;
            Opcion4CanvasColor.Color = Colors.Black;
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

        public void MarcarTerritorio(Path territorio)
        {
            territorio.Fill = Convertidor.GetSolidColorBrush(objJuego.JugadorTurnoActual().Color);
            territorio.Stroke = new SolidColorBrush(Colors.Goldenrod);
            territorio.StrokeThickness = 5;
        }

        public void DesmarcarTerritorio(Path territorio)
        {
            territorio.Fill = null;
            territorio.Stroke = new SolidColorBrush(Colors.Black);
            territorio.StrokeThickness = 1;
        }

        public void IniciarFortificacion()
        {
            MoverTropasGrid.Visibility = Visibility.Visible;
            TituloMoverTropas.Text = Constantes.MOVER_TROPAS_FORTIFICACION;
            MoverUnidadesTerritorioOrigen.Text = objJuego.TerritorioFortificacionOrigen.NombreTerritorio;
            MoverUnidadesTerritorioOrigenNUnidades.Text = objJuego.TerritorioFortificacionOrigen.NUnidadesDeplegadas.ToString();
            MoverUnidadesTerritorioDestino.Text = objJuego.TerritorioFortificacionDestino.NombreTerritorio;
            MoverUnidadesTerritorioDestinoNUnidades.Text = objJuego.TerritorioFortificacionDestino.NUnidadesDeplegadas.ToString();

            if (objJuego.TerritorioFortificacionOrigen.NUnidadesDeplegadas < Constantes.UnidadesMinimasParaEvolucionar)
                MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
            else
                MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
            if (objJuego.TerritorioFortificacionDestino.NUnidadesDeplegadas < Constantes.UnidadesMinimasParaEvolucionar)
                MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
            else
                MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));

            MoverUnidadesImgIzquierda.Visibility = Visibility.Collapsed;
            MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
        }

        public void ModificarCantidadUnidadesMover(int direccion)
        {
            int unidadesOrigen = int.Parse(MoverUnidadesTerritorioOrigenNUnidades.Text);
            int unidadesDestino = int.Parse(MoverUnidadesTerritorioDestinoNUnidades.Text);
            if (direccion == Constantes.Controles.IZQUIERDA)
            {
                if (unidadesOrigen + 1 > objJuego.TerritorioFortificacionOrigen.NUnidadesDeplegadas)
                {
                    return;
                }
                MoverUnidadesTerritorioOrigenNUnidades.Text = (unidadesOrigen + 1).ToString();
                MoverUnidadesTerritorioDestinoNUnidades.Text = (unidadesDestino - 1).ToString();
                unidadesOrigen++;
                unidadesDestino--;
                if (unidadesOrigen == Constantes.UnidadesMinimasParaEvolucionar)
                {
                    MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
                }
                if (unidadesDestino == (Constantes.UnidadesMinimasParaEvolucionar - 1))
                {
                    MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
                }
            }
            else if (direccion == Constantes.Controles.DERECHA)
            {
                if (unidadesOrigen - 1 <= 0)
                {
                    return;
                }
                MoverUnidadesTerritorioOrigenNUnidades.Text = (unidadesOrigen - 1).ToString();
                MoverUnidadesTerritorioDestinoNUnidades.Text = (unidadesDestino + 1).ToString();
                unidadesOrigen--;
                unidadesDestino++;
                if (unidadesOrigen == (Constantes.UnidadesMinimasParaEvolucionar - 1))
                {
                    MoverTropasOrigen.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidad));
                }
                if (unidadesDestino == Constantes.UnidadesMinimasParaEvolucionar)
                {
                    MoverTropasDestino.Source = new BitmapImage(new Uri(objJuego.JugadoresConectados[objJuego.TurnoActual].ImagenUnidadAgrupadora));
                }
            }
            if (unidadesOrigen == 1)
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Visible;
                MoverUnidadesImgDerecha.Visibility = Visibility.Collapsed;
            }
            else if (unidadesOrigen == objJuego.TerritorioFortificacionOrigen.NUnidadesDeplegadas)
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Collapsed;
                MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
            }
            else
            {
                MoverUnidadesImgIzquierda.Visibility = Visibility.Visible;
                MoverUnidadesImgDerecha.Visibility = Visibility.Visible;
            }
        }

        public void ActualizarDespuesDeFortificacion()
        {
            //Aca actualizar las unidades e fortificaocn
            var territorio = objJuego.Territorios[objJuego.TerritorioFortificacionOrigen.TerritorioId];
            ((TextBlock)FindName("UnidadCantidad" + territorio.NombreTerritorio)).Text = territorio.NUnidadesDeplegadas.ToString();
            ((Image)FindName("Unidad" + territorio.NombreTerritorio)).Source = new BitmapImage(new Uri(territorio.ImagenUnidades));

            territorio = objJuego.Territorios[objJuego.TerritorioFortificacionDestino.TerritorioId];
            ((TextBlock)FindName("UnidadCantidad" + territorio.NombreTerritorio)).Text = territorio.NUnidadesDeplegadas.ToString();
            ((Image)FindName("Unidad" + territorio.NombreTerritorio)).Source = new BitmapImage(new Uri(territorio.ImagenUnidades));
        }

        public async void IniciarSiguienteTurno()
        {
            foreach (var item in objJuego.JugadoresConectados)
            {
                if (!item.Ip.Equals(objJuego.IpJugadorTurnoActual))
                {
                    await App.objSDK.ConnectStreamSocket(new HostName(item.Ip));
                    await App.objSDK.StreamPing(Constantes.MesaConumicaDESHABILITARControles);
                }
            }
            DibujarJugadorEnTurno();
            ActualizarNumeroUnidadesParaDespliegue();
            contadorTimerSiguienteTurno = null;
            MensajeSiguienteTurnoColor.Color = Colors.Black;
            txtSiguienteTurnoMensaje.Text = "Es el turno de";
            txtJugadorSiguienteTurno.Text = objJuego.JugadoresConectados[objJuego.TurnoActual].Nombre;
            MensajeSiguienteTurno.Visibility = Visibility.Visible;
            timerSiguienteTurno.Change(0, 1000 * 1);
        }

        public async void timerSiguienteTurnoCallback(object sender)
        {
            if (contadorTimerSiguienteTurno == null)
            {
                contadorTimerSiguienteTurno = 4;
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                contadorTimerSiguienteTurno--;
                if (contadorTimerSiguienteTurno <= 0)
                {
                    timerSiguienteTurno.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorTimerSiguienteTurno = null;
                    MensajeSiguienteTurno.Visibility = Visibility.Collapsed;
                    //Habilitar controles del nuevo jugador en turno
                    EnviarMensajeJugador(objJuego.IpJugadorTurnoActual, Constantes.MesaConumicaHABILITARControles);
                }
                Seleccionar_Territorio(TerrSelec);
            });
        }

        public async void timerDerrotaJugadorCallback(object sender)
        {
            if (contadorTimerDerrotaJugador == null)
            {
                contadorTimerDerrotaJugador = 4;
            }
            await App.UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                contadorTimerDerrotaJugador--;
                if (contadorTimerDerrotaJugador <= 0)
                {
                    timerDerrotaJugador.Change(Timeout.Infinite, Timeout.Infinite);
                    contadorTimerDerrotaJugador = null;
                    MensajeSiguienteTurno.Visibility = Visibility.Collapsed;
                    //Verificar si el atacante ha ganado
                    if (objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorTurnoActual).Count() == 24)
                    {
                        //Mostrar cartel de victoria y fin de juego
                        txtJugadorGanador.Text = objJuego.JugadorTurnoActual().Nombre;
                        imgJugadorGanador.Source = new BitmapImage(new Uri(objJuego.JugadorTurnoActual().ImagenUnidad));
                        EnviarMensajeJugador(objJuego.IpJugadorTurnoActual, Constantes.MesaConumicaVICTORIAFinDelJuego);
                        GridVictoria.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        //Habilitar controles del nuevo jugador en turno
                        EnviarMensajeJugador(objJuego.IpJugadorTurnoActual, Constantes.MesaConumicaHABILITARControles);
                    }
                }
            });
        }

        private async void MiMetodoReceptorMesaJuegoHelper(string strIp, string strMensaje)
        {
            try
            {
                if (!string.IsNullOrEmpty(strIp) && !string.IsNullOrEmpty(strMensaje))
                {
                    var mensaje = strMensaje.Split(new string[] { Constantes.SEPARADOR }, StringSplitOptions.None);
                    #region El jugador ha presiona una tecla
                    if (mensaje[0] == Constantes.JugadorPresionaBoton)
                    {
                        #region Se recibe el mensaje (boton presionado)
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
                            #region Desplegar Unidades
                            if (objJuego.AccionActual == Constantes.AccionJuego.DESPLEGAR)
                            {
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 7)
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
                                    ActualizarNumeroUnidadesJugadorEnTurnoInfo();
                                    txtNroUnidadesParaDespliegue.Text = objJuego.UnidadesDisponiblesParaDesplegar.ToString();
                                    MapaTerritorioSeleccionadoUnidades.Text = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas.ToString();
                                    //La unidad evoluciona?
                                    if (objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NUnidadesDeplegadas == Constantes.UnidadesMinimasParaEvolucionar)
                                    {
                                        ((Image)FindName("Unidad" + objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].NombreTerritorio)).Source =
                                            new BitmapImage(new Uri(objJuego.JugadorTurnoActual().ImagenUnidadAgrupadora));
                                    }

                                    //Si el jugador se queda sin unidades para desplegar, mostrar el mensaje para continuar
                                    if (objJuego.UnidadesDisponiblesParaDesplegar == 0)
                                    {
                                        objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR_FIN_DESPLIGUE_CONTINUAR;
                                        txtMensajeContinuar.Text = Constantes.MensajesConfirmarContinuar.DespliegueContinuar;
                                        MensajeContinuar.Visibility = Visibility.Visible;
                                    }
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Despliegue y pasar a Ataque
                                    //Mostrar el mensaje de confirmacion
                                    objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR_FIN_DESPLIGUE_CONFIRMAR;
                                    txtMensajeContinuarCancelar.Text = Constantes.MensajesConfirmarContinuar.DespliegueConfirmar;
                                    MensajeContinuarCancelar.Visibility = Visibility.Visible;
                                }
                            }
                            #endregion
                            #region Despligue Mensaje de Continuar
                            else if (objJuego.AccionActual == Constantes.AccionJuego.DESPLEGAR_FIN_DESPLIGUE_CONTINUAR)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    MensajeContinuar.Visibility = Visibility.Collapsed;
                                    objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    btnFaseDespliegue.IsEnabled = false;
                                    btnFaseAtaque.IsEnabled = true;
                                    PanelUnidadesParaDesplegar.Visibility = Visibility.Collapsed;
                                }
                            }
                            #endregion
                            #region Despligue Mensaje de Confirmar
                            else if (objJuego.AccionActual == Constantes.AccionJuego.DESPLEGAR_FIN_DESPLIGUE_CONFIRMAR)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.FaseActual = Constantes.FaseJuego.ATAQUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    btnFaseDespliegue.IsEnabled = false;
                                    btnFaseAtaque.IsEnabled = true;
                                    PanelUnidadesParaDesplegar.Visibility = Visibility.Collapsed;
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;
                                }
                            }
                            #endregion
                        }
                        #endregion
                        #region ATAQUE
                        else if (objJuego.FaseActual == Constantes.FaseJuego.ATAQUE)
                        {
                            #region Elegir Origen Ataque
                            if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRORIGENATK)
                            {
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 7)
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
                                    objJuego.TerritorioAtaqueOrigen = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];
                                    objJuego.TerritorioAtaqueDestino = null;

                                    MarcarTerritorio(TerrSelec);

                                    //Cambiar a accion elegir destino ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    //Mostrar el mensaje de confirmacion
                                    auxAccionJuegoAnterior = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    objJuego.AccionActual = Constantes.AccionJuego.ATAQUE_FIN_ATAQUE_CONTINUAR;
                                    txtMensajeContinuarCancelar.Text = Constantes.MensajesConfirmarContinuar.AtaqueConfirmar;
                                    MensajeContinuarCancelar.Visibility = Visibility.Visible;
                                }
                            }
                            #endregion
                            #region Elegir DestinoAtaque
                            else if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRDESTINOATK)
                            {
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 7)
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

                                    if (!GameLogic.AtaqueLogic.TerritorioSePuedeAtacar(Territorio, objJuego.TerritorioAtaqueOrigen.TerritorioId, TerrSelec))
                                    {
                                        return;
                                    }

                                    //Se marca como territorio destino ataque
                                    objJuego.TerritorioAtaqueDestino = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];
                                    objJuego.IpJugadorDefiende = objJuego.TerritorioAtaqueDestino.IpJugadorPropietario;

                                    //Comienza la batalla
                                    objJuego.AccionActual = Constantes.AccionJuego.CONFIRMARATAQUE;
                                    InicializarGridBatallaAtaque();
                                    BatallaGrid.Visibility = Visibility.Visible;
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador cancela la seleccion se destino de ataque
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    DesmarcarTerritorio((Path)FindName(objJuego.TerritorioAtaqueOrigen.NombreTerritorio));
                                    objJuego.TerritorioAtaqueOrigen = null;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    //Mostrar el mensaje de confirmacion
                                    auxAccionJuegoAnterior = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.AccionActual = Constantes.AccionJuego.ATAQUE_FIN_ATAQUE_CONTINUAR;
                                    txtMensajeContinuarCancelar.Text = Constantes.MensajesConfirmarContinuar.AtaqueConfirmar;
                                    MensajeContinuarCancelar.Visibility = Visibility.Visible;
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
                                    // Habilitar controles adicionalmente hace vibrar el Windows Phone
                                    await App.objSDK.ConnectStreamSocket(new HostName(objJuego.IpJugadorDefiende));
                                    await App.objSDK.StreamPing(Constantes.MesaConumicaHABILITARControles);

                                    await App.objSDK.ConnectStreamSocket(new HostName(objJuego.IpJugadorTurnoActual));
                                    await App.objSDK.StreamPing(Constantes.MesaConumicaHABILITARControles);

                                    IniciarBatalla();
                                    objJuego.AccionActual = Constantes.AccionJuego.BATALLA_PRIMERA_PREGUNTA;
                                }
                                //Se cancela el ataque
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //El jugador cancela el inicio del ataque
                                    ActualizarDespuesDeBatalla();
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioAtaqueDestino = null;
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
                                    {
                                        if (respuestaAtacante != -1)
                                        {
                                            return;
                                        }
                                    }
                                    else if (respuestaDefensor != -1)
                                    {
                                        return;
                                    }
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
                                //Boton direccional
                                if (botonPresionado == Constantes.Controles.IZQUIERDA || botonPresionado == Constantes.Controles.DERECHA)
                                {
                                    ModificarUnidadesMover(botonPresionado);
                                }
                                //Se confirma el Movimiento de Tropas
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    int unidadesMover = int.Parse(MoverUnidadesTerritorioDestinoNUnidades.Text);
                                    objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas -= unidadesMover;
                                    objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas += unidadesMover;
                                    var jugador = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.TerritorioAtaqueOrigen.IpJugadorPropietario).First();
                                    if (objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                                        objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].ImagenUnidades = jugador.ImagenUnidadAgrupadora;
                                    else
                                        objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].ImagenUnidades = jugador.ImagenUnidad;
                                    if (objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                                        objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = jugador.ImagenUnidadAgrupadora;
                                    else
                                        objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ImagenUnidades = jugador.ImagenUnidad;
                                    objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].ColorUnidades = jugador.Color;
                                    objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].IpJugadorPropietario = jugador.Ip;
                                    objJuego.Territorios[objJuego.TerritorioAtaqueDestino.TerritorioId].NombreJugadorPropietario = jugador.Nombre;
                                    ActualizarDespuesDeBatalla();

                                    if (objJuego.Territorios[objJuego.TerritorioAtaqueOrigen.TerritorioId].NUnidadesDeplegadas <= 1)
                                    {
                                        objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                        DesmarcarTerritorio((Path)FindName(objJuego.TerritorioAtaqueOrigen.NombreTerritorio));
                                        objJuego.TerritorioAtaqueOrigen = null;
                                    }
                                    else
                                    {
                                        objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    }
                                    objJuego.TerritorioAtaqueDestino = null;
                                    MoverTropasGrid.Visibility = Visibility.Collapsed;
                                    BatallaGrid.Visibility = Visibility.Collapsed;

                                    //Verificar si el defensor ya no tiene territorios
                                    //Desntro del tiemer se verifica si el atacante ha ganado
                                    if (objJuego.Territorios.Where(x => x.IpJugadorPropietario == objJuego.IpJugadorDefiende).Count() == 0)
                                    {
                                        //Mostrar cartel de derrota y Notificar el jugador
                                        await App.objSDK.ConnectStreamSocket(new HostName(objJuego.IpJugadorTurnoActual));
                                        await App.objSDK.StreamPing(Constantes.MesaConumicaDESHABILITARControles);

                                        await App.objSDK.ConnectStreamSocket(new HostName(objJuego.IpJugadorDefiende));
                                        await App.objSDK.StreamPing(Constantes.MesaConumicaDERROTAFinDelJuego);

                                        objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorDefiende).First().Activo = false;
                                        txtSiguienteTurnoMensaje.Text = "Jugador eliminado";
                                        txtJugadorSiguienteTurno.Text = objJuego.JugadoresConectados.Where(x => x.Ip == objJuego.IpJugadorDefiende).First().Nombre;
                                        MensajeSiguienteTurnoColor.Color = Colors.Red;
                                        contadorTimerDerrotaJugador = null;
                                        MensajeSiguienteTurno.Visibility = Visibility.Visible;
                                        timerDerrotaJugador.Change(0, 1000 * 1);
                                    }

                                    objJuego.IpJugadorDefiende = "";
                                }
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
                                    ActualizarDespuesDeBatalla();
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOATK;
                                    objJuego.TerritorioAtaqueDestino = null;
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
                                    DesmarcarTerritorio((Path)FindName(objJuego.TerritorioAtaqueOrigen.NombreTerritorio));
                                    ActualizarDespuesDeBatalla();
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENATK;
                                    objJuego.TerritorioAtaqueOrigen = null;
                                    objJuego.TerritorioAtaqueDestino = null;
                                    BatallaGrid.Visibility = Visibility.Collapsed;
                                    objJuego.IpJugadorDefiende = "";
                                }
                            }
                            #endregion
                            #region ATAQUE - Mensaje de Confirmar terminar la FASE de Ataque
                            else if (objJuego.AccionActual == Constantes.AccionJuego.ATAQUE_FIN_ATAQUE_CONTINUAR)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.FaseActual = Constantes.FaseJuego.FORTIFICACION;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENFOR;
                                    btnFaseAtaque.IsEnabled = false;
                                    btnFaseFortificacion.IsEnabled = true;
                                    if (objJuego.TerritorioAtaqueOrigen != null)
                                    {
                                        DesmarcarTerritorio((Path)FindName(objJuego.TerritorioAtaqueOrigen.NombreTerritorio));
                                    }
                                    objJuego.TerritorioAtaqueOrigen = null;
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.AccionActual = auxAccionJuegoAnterior;
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
                                //Boton direccional
                                if (botonPresionado >= 0 && botonPresionado <= 7)
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

                                    //Marcar territorio como origen Fortificacion
                                    objJuego.TerritorioFortificacionOrigen = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];
                                    objJuego.TerritorioFortificacionDestino = null;
                                    MarcarTerritorio(TerrSelec);

                                    //Cambiar a accion elegir destino Fotificacion
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOFOR;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    //Mostrar el mensaje de confirmacion
                                    auxAccionJuegoAnterior = Constantes.AccionJuego.ELEGIRORIGENFOR;
                                    objJuego.AccionActual = Constantes.AccionJuego.FORTIFICAR_FIN_CONFIRMAR;
                                    txtMensajeContinuarCancelar.Text = Constantes.MensajesConfirmarContinuar.FortificarConfirmar;
                                    MensajeContinuarCancelar.Visibility = Visibility.Visible;
                                }
                            }
                            #endregion
                            #region Fortificar - Elegir el Destino de la fortificacion
                            else if (objJuego.AccionActual == Constantes.AccionJuego.ELEGIRDESTINOFOR)
                            {
                                //Boton direccional -- solo se puede mover si el territorio a mover le pertenece al jugador en turno
                                if (botonPresionado >= 0 && botonPresionado <= 7)
                                {
                                    var territorioASeleccionar = Territorio[Convert.ToInt32(TerrSelec.Tag), botonPresionado];
                                    if (territorioASeleccionar != null && objJuego.Territorios[Convert.ToInt32(territorioASeleccionar.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                    {
                                        MoverTerritorio(botonPresionado);
                                    }
                                }
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    //Verificar que no es el mismo territorio
                                    if (objJuego.TerritorioFortificacionOrigen.TerritorioId == objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].TerritorioId)
                                    {
                                        return;
                                    }

                                    //Verificar que el territorio perteneza al jugador
                                    if (!objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)].IpJugadorPropietario.Equals(mensaje[1]))
                                    {
                                        return;
                                    }

                                    //Marcar territorio como destino Fortificacion
                                    objJuego.TerritorioFortificacionDestino = objJuego.Territorios[Convert.ToInt32(TerrSelec.Tag)];

                                    //Cambiar a accion elegir destino Fotificacion
                                    objJuego.AccionActual = Constantes.AccionJuego.CONFIRMAR_FORTIFICACION;
                                    IniciarFortificacion();
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    DesmarcarTerritorio((Path)FindName(objJuego.TerritorioFortificacionOrigen.NombreTerritorio));
                                    objJuego.TerritorioFortificacionOrigen = null;
                                    objJuego.TerritorioFortificacionDestino = null;

                                    //Cambiar a accion elegir destino Fotificacion
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRORIGENFOR;
                                }
                                else if (botonPresionado == Constantes.Controles.TRIANGULO)
                                {
                                    //El jugador está pidiendo terminar la fase de Ataque y pasar a Fortificacion
                                    //Mostrar el mensaje de confirmacion
                                    auxAccionJuegoAnterior = Constantes.AccionJuego.ELEGIRDESTINOFOR;
                                    objJuego.AccionActual = Constantes.AccionJuego.FORTIFICAR_FIN_CONFIRMAR;
                                    txtMensajeContinuarCancelar.Text = Constantes.MensajesConfirmarContinuar.FortificarConfirmar;
                                    MensajeContinuarCancelar.Visibility = Visibility.Visible;
                                }
                            }
                            #endregion
                            #region Fortificar - Realizar acción
                            else if (objJuego.AccionActual == Constantes.AccionJuego.CONFIRMAR_FORTIFICACION)
                            {
                                //Boton direccional -- solo se puede mover si el territorio a mover le pertenece al jugador en turno
                                if (botonPresionado == Constantes.Controles.IZQUIERDA || botonPresionado == Constantes.Controles.DERECHA)
                                {
                                    //Agregar o disminuir numero de undiades a mover
                                    ModificarCantidadUnidadesMover(botonPresionado);
                                }
                                else if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    //Confirmar el movimiento de unidades para fortificacion
                                    int unidadesOrigen = int.Parse(MoverUnidadesTerritorioOrigenNUnidades.Text);
                                    int unidadesDestino = int.Parse(MoverUnidadesTerritorioDestinoNUnidades.Text);
                                    objJuego.Territorios[objJuego.TerritorioFortificacionOrigen.TerritorioId].NUnidadesDeplegadas = unidadesOrigen;
                                    objJuego.Territorios[objJuego.TerritorioFortificacionDestino.TerritorioId].NUnidadesDeplegadas = unidadesDestino;

                                    var jugador = objJuego.JugadorTurnoActual();
                                    if (objJuego.Territorios[objJuego.TerritorioFortificacionOrigen.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                                        objJuego.Territorios[objJuego.TerritorioFortificacionOrigen.TerritorioId].ImagenUnidades = jugador.ImagenUnidadAgrupadora;
                                    else
                                        objJuego.Territorios[objJuego.TerritorioFortificacionOrigen.TerritorioId].ImagenUnidades = jugador.ImagenUnidad;
                                    if (objJuego.Territorios[objJuego.TerritorioFortificacionDestino.TerritorioId].NUnidadesDeplegadas >= Constantes.UnidadesMinimasParaEvolucionar)
                                        objJuego.Territorios[objJuego.TerritorioFortificacionDestino.TerritorioId].ImagenUnidades = jugador.ImagenUnidadAgrupadora;
                                    else
                                        objJuego.Territorios[objJuego.TerritorioFortificacionDestino.TerritorioId].ImagenUnidades = jugador.ImagenUnidad;
                                    ActualizarDespuesDeFortificacion();
                                    MoverTropasGrid.Visibility = Visibility.Collapsed;

                                    //Finalizar Truno
                                    objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;
                                    btnFaseFortificacion.IsEnabled = false;
                                    btnFaseDespliegue.IsEnabled = true;
                                    if (objJuego.TerritorioFortificacionOrigen != null)
                                    {
                                        DesmarcarTerritorio((Path)FindName(objJuego.TerritorioFortificacionOrigen.NombreTerritorio));
                                    }
                                    objJuego.TerritorioFortificacionOrigen = null;
                                    //Se acaba el turno del jugador
                                    GameLogic.TurnoLogic.TerminarTurnoComenzarNuevoTurno(objJuego);
                                    IniciarSiguienteTurno();
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    //Cancelar la fortificacion actual
                                    MoverTropasGrid.Visibility = Visibility.Collapsed;
                                    objJuego.FaseActual = Constantes.FaseJuego.FORTIFICACION;
                                    objJuego.AccionActual = Constantes.AccionJuego.ELEGIRDESTINOFOR;
                                    objJuego.TerritorioFortificacionDestino = null;
                                }
                            }
                            #endregion
                            #region Fortificar - Mensaje de Confirmar
                            else if (objJuego.AccionActual == Constantes.AccionJuego.FORTIFICAR_FIN_CONFIRMAR)
                            {
                                if (botonPresionado == Constantes.Controles.EQUIS)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.FaseActual = Constantes.FaseJuego.DESPLIEGUE;
                                    objJuego.AccionActual = Constantes.AccionJuego.DESPLEGAR;
                                    btnFaseFortificacion.IsEnabled = false;
                                    btnFaseDespliegue.IsEnabled = true;
                                    if (objJuego.TerritorioFortificacionOrigen != null)
                                    {
                                        DesmarcarTerritorio((Path)FindName(objJuego.TerritorioFortificacionOrigen.NombreTerritorio));
                                    }
                                    objJuego.TerritorioFortificacionOrigen = null;
                                    //Se acaba el turno del jugador
                                    GameLogic.TurnoLogic.TerminarTurnoComenzarNuevoTurno(objJuego);
                                    IniciarSiguienteTurno();
                                }
                                else if (botonPresionado == Constantes.Controles.CIRCULO)
                                {
                                    MensajeContinuarCancelar.Visibility = Visibility.Collapsed;
                                    objJuego.AccionActual = auxAccionJuegoAnterior;
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion
                    #region Jugador se une al juego iniciado
                    else if (mensaje[0] == Constantes.UnirseEnviameConfirmacion)
                    {
                        //mensaje[0] => Acción (UnirseEnviameConfirmacion)
                        //mensaje[1] => mesaSeleccionada
                        //mensaje[2] => objJugador.Ip
                        //mensaje[3] => objJugador.Nombre
                        //mensaje[4] => strBytes (imagen del jugador)
                        if (mensaje.Length != 5)
                            return;

                        //Verificar que es la mesa seleccionada
                        if (mensaje[1] != objJuego.JuegoID)
                            return;

                        var jugador = objJuego.JugadoresConectados.FirstOrDefault(x => x.Ip == mensaje[2]);
                        //Verificar que el jugador se encuentraba en el juego
                        if (jugador == null)
                            return;

                        var flagTurno = objJuego.IpJugadorTurnoActual == jugador.Ip ? "1" : "0";
                        //Notificar al nuevo jugador que se ha unido a la mesa
                        await App.objSDK.ConnectStreamSocket(new HostName(mensaje[2]));
                        await App.objSDK.StreamPing(Constantes.ConfirmacionUnirseMesaJuego + Constantes.SEPARADOR +
                            objJuego.Ip + Constantes.SEPARADOR +
                            jugador.Color + Constantes.SEPARADOR +
                            flagTurno);
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

        private void btnMenuPrincipal_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPrincipal));
        }

        private async void EnviarMensajeJugador(string ip, string mensaje)
        {
            await App.objSDK.ConnectStreamSocket(new HostName(ip));
            await App.objSDK.StreamPing(mensaje);
        }

        private void Jugador1Imagen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IniciarSDK();
            if (objJuego.IpJugadorTurnoActual == objJuego.JugadoresConectados[0].Ip)
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[0].Ip, Constantes.MesaConumicaHABILITARControles);
            }
            else
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[0].Ip, Constantes.MesaConumicaDESHABILITARControles);
            }
        }

        private void Jugador2Imagen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IniciarSDK();
            if (objJuego.IpJugadorTurnoActual == objJuego.JugadoresConectados[1].Ip)
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[1].Ip, Constantes.MesaConumicaHABILITARControles);
            }
            else
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[1].Ip, Constantes.MesaConumicaDESHABILITARControles);
            }
        }

        private void Jugador3Imagen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IniciarSDK();
            if (objJuego.IpJugadorTurnoActual == objJuego.JugadoresConectados[2].Ip)
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[2].Ip, Constantes.MesaConumicaHABILITARControles);
            }
            else
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[2].Ip, Constantes.MesaConumicaDESHABILITARControles);
            }
        }

        private void Jugador4Imagen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            IniciarSDK();
            if (objJuego.IpJugadorTurnoActual == objJuego.JugadoresConectados[3].Ip)
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[3].Ip, Constantes.MesaConumicaHABILITARControles);
            }
            else
            {
                EnviarMensajeJugador(objJuego.JugadoresConectados[3].Ip, Constantes.MesaConumicaDESHABILITARControles);
            }
        }
    }
}
